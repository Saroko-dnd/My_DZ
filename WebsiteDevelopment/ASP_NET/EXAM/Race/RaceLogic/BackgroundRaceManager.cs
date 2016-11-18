using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceLogic
{
    public class BackgroundRaceManager : IBackgroundRaceManager
    {
        private IRaceRepository CurrentRaceRepository;
        private IAccessorToRaceInfo CurrentAccessorToRaceInfo;

        public void StartBackgroundRaceManagement()
        {
            while (CurrentAccessorToRaceInfo.Winner == null)
            {
                foreach (Racer CurrentRacer in CurrentRaceRepository.AllRacers)
                {
                    CurrentRacer.DistanceCoveredInKm += CurrentRacer.CarSpeedKph;
                }
                CurrentRaceRepository.SaveAllChanges();
                Thread.Sleep(3000);
                CurrentAccessorToRaceInfo.Winner = CurrentRaceRepository.AllRacers.Where(FoundRacer => FoundRacer.DistanceCoveredInKm >= CurrentAccessorToRaceInfo.FinishDistance).FirstOrDefault();
            }

            CurrentAccessorToRaceInfo.NewRaceCanBeCreated = true;
        }

        public BackgroundRaceManager(IRaceRepository NewRaceRepository, IAccessorToRaceInfo NewAccessorToRaceInfo)
        {
            CurrentRaceRepository = NewRaceRepository;
            CurrentAccessorToRaceInfo = NewAccessorToRaceInfo;
        }
    }
}

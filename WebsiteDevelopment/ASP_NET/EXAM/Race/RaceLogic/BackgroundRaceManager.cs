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
        private IRaceUnitOfWork CurrentRaceUnitOfWork;
        private IAccessorToRaceInfo CurrentAccessorToRaceInfo;

        public void StartBackgroundRaceManagement()
        {
            while (CurrentAccessorToRaceInfo.Winner == null)
            {
                foreach (Racer CurrentRacer in CurrentRaceUnitOfWork.RacerRepository.GetAll())
                {
                    CurrentRacer.DistanceCoveredInKm += CurrentRacer.CarSpeedKph;
                }
                CurrentRaceUnitOfWork.SaveAllChanges();
                Thread.Sleep(3000);
                CurrentAccessorToRaceInfo.Winner = CurrentRaceUnitOfWork.RacerRepository.GetAll().Where(FoundRacer => FoundRacer.DistanceCoveredInKm >= CurrentAccessorToRaceInfo.FinishDistance).
                    FirstOrDefault();
            }

            CurrentAccessorToRaceInfo.NewRaceCanBeCreated = true;
        }

        public BackgroundRaceManager(IRaceUnitOfWork NewRaceUnitOfWork, IAccessorToRaceInfo NewAccessorToRaceInfo)
        {
            CurrentRaceUnitOfWork = NewRaceUnitOfWork;
            CurrentAccessorToRaceInfo = NewAccessorToRaceInfo;
        }
    }
}

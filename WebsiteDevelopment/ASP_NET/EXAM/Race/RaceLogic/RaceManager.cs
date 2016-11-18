using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceLogic
{
    public class RaceManager : IRaceManager
    {

        private IRaceRepository CurrentRaceRepository;
        private IAccessorToRaceInfo CurrentAccessorToRaceInfo;
        private IBackgroundRaceManager CurrentBackgroundRaceManager;

        public Racer Winner
        {
            get
            {
                return CurrentAccessorToRaceInfo.Winner;
            }

            private set
            {
                CurrentAccessorToRaceInfo.Winner = value;
            }
        }

        public bool NewRaceCanBeCreated
        {
            get
            {
                return CurrentAccessorToRaceInfo.NewRaceCanBeCreated;
            }

            private set
            {
                CurrentAccessorToRaceInfo.NewRaceCanBeCreated = value;
            }
        }

        public long CurrentFinishDistance
        {
            get
            {
                return CurrentAccessorToRaceInfo.FinishDistance;
            }

            private set
            {
                CurrentAccessorToRaceInfo.FinishDistance = value;
            }
        }

        public IRaceRepository RaceRepository
        {
            get
            {
                return CurrentRaceRepository;
            }
        }

        public void StartRaceManagementAsync(long NewFinishDistance)
        {
            NewRaceCanBeCreated = false;
            Winner = null;
            CurrentFinishDistance = NewFinishDistance;
            foreach (Racer CurrentRacer in CurrentRaceRepository.AllRacers)
            {
                CurrentRacer.DistanceCoveredInKm = 0;
            }
            CurrentRaceRepository.SaveAllChanges();

            ThreadPool.QueueUserWorkItem(o => CurrentBackgroundRaceManager.StartBackgroundRaceManagement());
        }

        public RaceManager(IRaceRepository NewRaceRepository, IAccessorToRaceInfo NewAccessorToRaceInfo, IBackgroundRaceManager NewBackgroundRaceManager)
        {
            CurrentRaceRepository = NewRaceRepository;
            CurrentAccessorToRaceInfo = NewAccessorToRaceInfo;
            CurrentBackgroundRaceManager = NewBackgroundRaceManager;
        }
    }
}

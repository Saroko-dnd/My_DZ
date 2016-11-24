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

        private IRaceUnitOfWork CurrentRaceUnitOfWork;
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

        public IRaceUnitOfWork RaceRepositories
        {
            get
            {
                return CurrentRaceUnitOfWork;
            }
        }

        public void StartRaceManagementAsync(long NewFinishDistance)
        {
            NewRaceCanBeCreated = false;
            Winner = null;
            CurrentFinishDistance = NewFinishDistance;
            foreach (Racer CurrentRacer in CurrentRaceUnitOfWork.RacerRepository.GetAll())
            {
                CurrentRacer.DistanceCoveredInKm = 0;
            }
            CurrentRaceUnitOfWork.SaveAllChanges();

            ThreadPool.QueueUserWorkItem(o => CurrentBackgroundRaceManager.StartBackgroundRaceManagement());
        }

        public RaceManager(IRaceUnitOfWork NewRaceUnitOfWork, IAccessorToRaceInfo NewAccessorToRaceInfo, IBackgroundRaceManager NewBackgroundRaceManager)
        {
            CurrentRaceUnitOfWork = NewRaceUnitOfWork;
            CurrentAccessorToRaceInfo = NewAccessorToRaceInfo;
            CurrentBackgroundRaceManager = NewBackgroundRaceManager;
        }
    }
}

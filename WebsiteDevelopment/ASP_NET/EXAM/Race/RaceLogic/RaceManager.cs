﻿using RaceInfrastructure;
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

        private void BackgroundRaceManagement()
        {
            List<Racer> CurrentListOfRacers;
            while (Winner == null)
            {
                CurrentListOfRacers = CurrentRaceRepository.AllRacers.ToList();
                foreach (Racer CurrentRacer in CurrentListOfRacers)
                {
                    CurrentRacer.DistanceCoveredInKm += CurrentRacer.CarSpeedKph;
                }
                CurrentRaceRepository.SaveAllChanges();
                Thread.Sleep(1000);
                Winner = CurrentRaceRepository.AllRacers.Where(FoundRacer => FoundRacer.DistanceCoveredInKm >= CurrentFinishDistance).FirstOrDefault();
            }

            NewRaceCanBeCreated = true;
        }

        public void StartRaceManagementAsync(long NewFinishDistance)
        {
            NewRaceCanBeCreated = false;
            Winner = null;
            CurrentFinishDistance = NewFinishDistance;
            List<Racer> CurrentListOfRacers;
            CurrentListOfRacers = CurrentRaceRepository.AllRacers.ToList();
            foreach (Racer CurrentRacer in CurrentListOfRacers)
            {
                CurrentRacer.DistanceCoveredInKm = 0;
            }
            CurrentRaceRepository.SaveAllChanges();

            ThreadPool.QueueUserWorkItem(o => this.BackgroundRaceManagement());
        }

        public RaceManager(IRaceRepository NewRaceRepository, IAccessorToRaceInfo NewAccessorToRaceInfo)
        {
            CurrentRaceRepository = NewRaceRepository;
            CurrentAccessorToRaceInfo = NewAccessorToRaceInfo;
        }
    }
}

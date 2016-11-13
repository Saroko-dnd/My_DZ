using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RaceWebsite.ClassesForRaceWebsite
{
    public class RaceBackgroundWorker
    {
        IAccessorToRaceInfo CurrentAccessorToRaceInfo;
        IRaceRepository CurrentRaceRepository;

        public void StartRaceManagement()
        {
            CurrentRaceRepository = DependencyResolver.Current.GetService<IRaceRepository>();
            Car Winner = null;
            CurrentAccessorToRaceInfo.FinishDistance = 500;

            foreach (Car CurrentCar in CurrentRaceRepository.AllCars)
            {
                CurrentCar.DistanceCovered = 0;
            }
            CurrentRaceRepository.SaveAllChanges();

            while (Winner == null)
            {               
                foreach (Car CurrentCar in CurrentRaceRepository.AllCars)
                {
                    CurrentCar.DistanceCovered += CurrentCar.Speed;
                }
                CurrentRaceRepository.SaveAllChanges();
                Thread.Sleep(1000);
                Winner = CurrentRaceRepository.AllCars.Where(FoundCar => FoundCar.DistanceCovered >= CurrentAccessorToRaceInfo.FinishDistance).FirstOrDefault();
            }

            CurrentAccessorToRaceInfo.NewRaceCanBeCreated = true;
        }

        public RaceBackgroundWorker()
        {
            CurrentAccessorToRaceInfo = DependencyResolver.Current.GetService<IAccessorToRaceInfo>();
        }
    }
}
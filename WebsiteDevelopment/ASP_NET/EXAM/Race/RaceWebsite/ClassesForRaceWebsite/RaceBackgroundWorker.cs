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

        }

        public RaceBackgroundWorker()
        {
            CurrentAccessorToRaceInfo = DependencyResolver.Current.GetService<IAccessorToRaceInfo>();
        }
    }
}
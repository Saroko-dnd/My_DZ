using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.ClassesForRaceWebsite
{
    public static class ApplicationConstants
    {
        public static readonly string ConnectionStringName = "RaceWebsiteDB_NewVersion";
        public static readonly string PathFromRouteToCarPartialView = "~/Views/Shared/CarPartialView.cshtml";
        public static readonly string PathFromRouteToCarsCollectionPartialView = "~/Views/Shared/CarsCollectionPartialView.cshtml";
    }
}
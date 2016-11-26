using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.ClassesForRaceWebsite
{
    public static class ApplicationConstants
    {
        public static readonly string ConnectionStringName = "RaceWebsiteDB_NewVersion";
        public static readonly string PathFromRouteToRaceParticipantsPartialView = "~/Views/Shared/RaceParticipantsPartialView.cshtml";
        public static readonly string PathFromRouteToRacerInfoView = "~/Views/Home/RacerInfo.cshtml";
        public static readonly string PathFromRouteToRacerInfoDuringRacePartialView = "~/Views/Shared/RacerInfoDuringRace.cshtml";
        public static readonly int MaxCarSpeed = 400;
        public static readonly int MinCarSpeed = 100;
        public static readonly long MaxValueForFinishDistance = long.MaxValue;
        public static readonly long MinValueForFinishDistance = 100;
        public static readonly int MaxLengthOfNames = 30;
        public static readonly int MaxLengthOfBigTexts = 4000;
    }
}
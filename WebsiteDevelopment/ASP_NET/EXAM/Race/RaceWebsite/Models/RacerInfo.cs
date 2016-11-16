using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.Models
{
    public class RacerInfo
    {
        public bool EditingAllowed { get; set; }
        public Racer SelectedRacer { get; set; }

        public RacerInfo(bool EnableEditingForRacerInfo, Racer NewSelectedRacer)
        {
            EditingAllowed = EnableEditingForRacerInfo;
            SelectedRacer = NewSelectedRacer;
        }

    }
}
using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.Models
{
    public class RaceParticipantsModel
    {
        public IRaceManager CurrentRaceManager { get; set; }
        public bool AdminIsHere { get; set; }

        public RaceParticipantsModel(bool ThisIsAdminPage, IRaceManager NewRaceManager)
        {
            AdminIsHere = ThisIsAdminPage;
            CurrentRaceManager = NewRaceManager;
        }
    }
}
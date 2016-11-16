using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLogic
{
    public class AccessorToRaceInfo : IAccessorToRaceInfo
    {
        public long FinishDistance { get; set; }
        public bool NewRaceCanBeCreated { get; set; }
        public Racer Winner { get; set; }

        public AccessorToRaceInfo()
        {
            Winner = null;
            FinishDistance = 0;
            NewRaceCanBeCreated = true;
        }
    }
}

using RaceInfrastructure.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IAccessorToRaceInfo
    {
        bool NewRaceCanBeCreated { get; set; }
        long FinishDistance { get; set; }
        Racer Winner { get; set; }
    }
}

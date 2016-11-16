using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IRaceRepository
    {
        IQueryable<Racer> AllRacers { get; }
        void AddNewRacer(Racer NewRacer);
        void SaveAllChanges();
    }
}

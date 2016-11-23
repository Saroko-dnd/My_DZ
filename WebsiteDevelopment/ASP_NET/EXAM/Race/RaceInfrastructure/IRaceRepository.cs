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
        IQueryable<User> Users { get; }
        IQueryable<Role> Roles { get; }
        void AddNewRacer(Racer NewRacer);
        void UpdateRacerWithSameId(Racer UpdatedRacer);
        void SaveAllChanges();
    }
}

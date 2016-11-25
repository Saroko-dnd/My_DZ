using RaceInfrastructure;
using RaceInfrastructure.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess.Repositories
{
    internal class RacerRepository : Repository<Racer>
    {
        internal RacerRepository(RaceApplicationDataContext NewDataContext)
            : base(NewDataContext)
        {

        }
    }
}

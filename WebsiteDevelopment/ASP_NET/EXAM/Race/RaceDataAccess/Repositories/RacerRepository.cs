using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess.Repositories
{
    public class RacerRepository : Repository<Racer>
    {
        public RacerRepository(RaceApplicationDataContext NewDataContext)
            : base(NewDataContext)
        {

        }
    }
}

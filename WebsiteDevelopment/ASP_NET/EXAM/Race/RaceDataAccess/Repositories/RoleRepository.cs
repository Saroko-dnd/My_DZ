using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(RaceApplicationDataContext NewDataContext)
            : base(NewDataContext)
        {

        }
    }
}

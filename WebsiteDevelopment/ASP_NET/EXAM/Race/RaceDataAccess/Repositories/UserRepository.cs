using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(RaceApplicationDataContext NewDataContext)
            : base(NewDataContext)
        {

        }
    }
}

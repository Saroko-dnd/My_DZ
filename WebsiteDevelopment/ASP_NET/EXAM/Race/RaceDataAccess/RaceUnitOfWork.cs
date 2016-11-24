using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaceInfrastructure.Repositories;
using System.Threading;
using RaceDataAccess.Repositories;

namespace RaceDataAccess
{
    public class RaceUnitOfWork : IRaceUnitOfWork
    {
        private readonly RaceApplicationDataContext currentDataContext;
        private IRepository<Racer> racerRepository;
        private IRepository<Role> roleRepository;
        private IRepository<User> userRepository;

        public RaceUnitOfWork(string ConnectionStringName)
        {
            currentDataContext = new RaceApplicationDataContext(ConnectionStringName);
        }

        public IRepository<Racer> RacerRepository
        {
            get
            {
                return racerRepository ?? (racerRepository = new RacerRepository(currentDataContext));
            }
        }

        public IRepository<Role> RoleRepository
        {
            get
            {
                return roleRepository ?? (roleRepository = new RoleRepository(currentDataContext));
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                return userRepository ?? (userRepository = new UserRepository(currentDataContext));
            }
        }

        public void Dispose()
        {
            racerRepository = null;
            roleRepository = null;
            userRepository = null;
            currentDataContext.Dispose();
        }

        public int SaveAllChanges()
        {
            return currentDataContext.SaveChanges();
        }

        public Task<int> SaveAllChangesAsync()
        {
            return currentDataContext.SaveChangesAsync();
        }

        public Task<int> SaveAllChangesAsync(CancellationToken CurrentCancellationToken)
        {
            return currentDataContext.SaveChangesAsync(CurrentCancellationToken);
        }
    }
}

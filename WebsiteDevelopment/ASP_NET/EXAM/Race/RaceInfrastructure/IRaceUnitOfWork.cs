using RaceInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IRaceUnitOfWork : IDisposable
    {
        IRepository<Racer> RacerRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }

        int SaveAllChanges();
        Task<int> SaveAllChangesAsync();
        Task<int> SaveAllChangesAsync(CancellationToken CurrentCancellationToken);
    }
}

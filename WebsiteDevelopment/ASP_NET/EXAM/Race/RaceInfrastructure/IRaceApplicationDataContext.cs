using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IRaceApplicationDataContext
    {
        int SaveAllChanges();
        Task<int> SaveAllChangesAsync();
        Task<int> SaveAllChangesAsync(CancellationToken CurrentCancellationToken);
    }
}

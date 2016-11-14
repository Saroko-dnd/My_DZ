using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IRaceManager
    {
        Car Winner { get; }
        bool NewRaceCanBeCreated { get; }
        long CurrentFinishDistance { get; }
        void StartRaceManagementAsync(long CurrentFinishDistance);
        IEnumerable<Car> GetAllCars();
    }
}

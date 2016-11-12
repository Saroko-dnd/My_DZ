using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IRaceManager
    {
        Car Winner { get; set; }
        IEnumerable<Car> GetAllCars { get; set;}
        void MoveCars();
        void StopRace();
        void ResumeRace();
        void RestartRace();
    }
}

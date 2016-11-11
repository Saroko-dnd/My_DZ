using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface IRace
    {
        Car Winner { get; set; }
        IEnumerable<Car> Cars {get; set;}
        void MoveCars();
        void StopRace();
        void ResumeRace();
        void RestartRace();
    }
}

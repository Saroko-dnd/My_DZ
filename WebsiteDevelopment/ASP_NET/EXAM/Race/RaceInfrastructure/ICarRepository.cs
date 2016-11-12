using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public interface ICarRepository
    {
        IEnumerable<Car> AllCars { get;}
        void AddNewCar(Car NewCar);
    }
}

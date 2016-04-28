using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternBuilder.Builders
{
    public interface ICarBuilder
    {
        void CreateNewCar();
        void CreateBody();
        void CreateEngine();
        void CreateWheels();
        void CreateTransmission();
        Car GetResult(); 
    }
}

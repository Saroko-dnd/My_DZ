using PatternBuilder.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternBuilder
{
    public class CarFactory
    {
        public Car Construct(ICarBuilder CurrentBuilder)
        {
            CurrentBuilder.CreateNewCar();
            CurrentBuilder.CreateBody();
            CurrentBuilder.CreateWheels();
            CurrentBuilder.CreateEngine();
            CurrentBuilder.CreateTransmission();
            return CurrentBuilder.GetResult();
        }
    }
}

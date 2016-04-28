using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternBuilder.Builders
{
    public class FordProbeBuilder : ICarBuilder
    {
        private Car CurrentCar;
        public void CreateNewCar()
        {
            CurrentCar = new Car();
        }
        public void CreateBody()
        {
            CurrentCar.Body = "Купе";
        }
        public void CreateEngine()
        {
            CurrentCar.Engine = 160;
        }
        public void CreateWheels()
        {
            CurrentCar.Wheels = 14;
        }
        public void CreateTransmission()
        {
            CurrentCar.Transmission = "4 Auto";
        }
        public Car GetResult()
        {
            return CurrentCar;
        }
    }
}

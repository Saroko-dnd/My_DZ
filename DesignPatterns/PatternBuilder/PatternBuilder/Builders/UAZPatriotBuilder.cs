using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternBuilder.Builders
{
    public class UAZPatriotBuilder : ICarBuilder
    {
        private Car CurrentCar;
        public void CreateNewCar()
        {
            CurrentCar = new Car();
        }
        public void CreateBody()
        {
            CurrentCar.Body = "Универсал";
        }
        public void CreateEngine()
        {
            CurrentCar.Engine = 120;
        }
        public void CreateWheels()
        {
            CurrentCar.Wheels = 16;
        }
        public void CreateTransmission()
        {
            CurrentCar.Transmission = "4 Manual";
        }
        public Car GetResult()
        {
            return CurrentCar;
        }
    }
}

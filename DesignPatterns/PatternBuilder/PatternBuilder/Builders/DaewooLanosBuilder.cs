using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternBuilder.Builders
{
    public class DaewooLanosBuilder : ICarBuilder
    {
        private Car CurrentCar;
        public void CreateNewCar()
        {
            CurrentCar = new Car();
        }
        public void CreateBody()
        {
            CurrentCar.Body = "Седан";
        }
        public void CreateEngine()
        {
            CurrentCar.Engine = 98;
        }
        public void CreateWheels()
        {
            CurrentCar.Wheels = 13;
        }
        public void CreateTransmission()
        {
            CurrentCar.Transmission = "5 Manual";
        }
        public Car GetResult()
        {
            return CurrentCar;
        }
    }
}

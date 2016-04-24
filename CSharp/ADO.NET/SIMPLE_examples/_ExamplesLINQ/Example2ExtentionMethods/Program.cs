using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example2ExtentionMethods
{
    static class Program
    {
        public static void Main()
        {
            Car car = new Car { Name = "BMW", Num = 1, Fuel = 50 };
            Console.WriteLine(car);
            car.Filling(100);
            Console.WriteLine(car);
            string state = car.FuelControl();
            Console.WriteLine(state);
            Console.ReadKey();
        }

        public static string FuelControl(this Car car)
        {
            if (car.Fuel <= 0)
                return "Закончилось топливо";
            return "Все ОК!";
        }
    }

    class Car
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public double Fuel { get; set; }

        public void Go()
        {
            /// Метод ехать !
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} литров", Name, Fuel);
        }
    }

    static class FuelStation
    {
        public static void Filling(this Car car, double fuel)
        {
            car.Fuel += fuel;
        }
    }  
}

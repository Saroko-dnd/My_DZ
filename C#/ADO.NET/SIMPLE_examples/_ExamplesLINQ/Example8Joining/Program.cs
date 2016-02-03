using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example4Joining
{
    // Ресурсы:
    //  http://www.codeproject.com/Articles/488643/LinQ-Extended-Joins
    // 

   


    class Program
    {   // Объединение данных из различных коллекций (таблиц)!
        static void Main(string[] args)
        {
            List<Car> Cars = CreateCars();
            List<Driver> Drivers = CreateDrivers();

            // Получение данных из двух коллекций с использованием операторов запроса
            var Info1 = from c in Cars               // 1-ая коллекция
                        join d in Drivers            // 2-ая коллекция
                        on c.DriverID equals d.ID    // связь 
                        select new                   // формирование анонимного типа
                        {
                            Name = d.Name,
                            Price = c.Price
                        };

            foreach (var item in Info1)
                Console.WriteLine(item);

            Console.WriteLine(new string('*', 40));

            // Получение данных из двух коллекций с расширяющих методов
            var Info2 = Cars.Join(Drivers,
                                    c => c.DriverID,
                                    d => d.ID,
                                    (c, d) => new { car = c, driver = d }).
                                    Select((p) => new
                                    {
                                        Name = p.driver.Name,
                                        Price = p.car.Price
                                    });

            foreach (var item in Info2)
                Console.WriteLine(item);


            Console.ReadKey();
        }

            static List<Car> CreateCars()
            {
                return new List<Car>
                {
                    new Car(){ID=1, Name="BMW", Price=25000, DriverID=1},
                        new Car(){ID=2, Name="Ford", Price=15000, DriverID=2},
                        new Car(){ID=3, Name="Opel", Price=1900, DriverID=2},
                        new Car(){ID=4, Name="Audi", Price=2200, DriverID=3},
                };
            }

            static List<Driver> CreateDrivers()
            {
                return new List<Driver>
                {
                    new Driver(){ID=1, Name="Alex", Phone="+555-135246"},
                        new Driver(){ID=2, Name="Ivan", Phone="+555-123456"},
                        new Driver(){ID=3, Name="Inna", Phone="+555-654321"},
                        new Driver(){ID=4, Name="Bobr", Phone="+555-246135"},
                };
            }
    }




    public class Driver
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class Car
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int DriverID { get; set; }
    }
}

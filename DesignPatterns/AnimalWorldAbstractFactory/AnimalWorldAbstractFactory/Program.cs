using AnimalWorldAbstractFactory.AbstractFabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorldAbstractFactory
{
    class Program
    {
        public static Random MainRandom = new Random();

        static void Main(string[] args)
        {
            WorldOfAnimals TestWorld = new WorldOfAnimals(Africa.Create(), 5, 50);
            Console.WriteLine("Simulation start!\r\n");
            TestWorld.Simulation(5);
            Console.WriteLine("Simulation end!");
            Console.ReadKey();
        }
    }
}

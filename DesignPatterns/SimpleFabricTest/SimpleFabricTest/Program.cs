using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fabric = SimpleFabric;

namespace SimpleFabricTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Fabric.IAirplane> ListOfAirplanes = new List<Fabric.IAirplane>();
            ListOfAirplanes.Add(Fabric.Airplanefactory.CreateAirplane(Fabric.Airplanes.Airliner));
            ListOfAirplanes.Add(Fabric.Airplanefactory.CreateAirplane(Fabric.Airplanes.CargoAirplane));
            ListOfAirplanes.Add(Fabric.Airplanefactory.CreateAirplane(Fabric.Airplanes.SingleSeater));
            foreach (Fabric.IAirplane CurrentAirplane in ListOfAirplanes)
            {
                Console.WriteLine(CurrentAirplane.GetInfo() + "\r\n");
            }
            Console.ReadKey();
        }
    }
}

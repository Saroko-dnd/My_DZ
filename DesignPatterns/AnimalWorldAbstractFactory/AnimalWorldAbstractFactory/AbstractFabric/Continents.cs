using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorldAbstractFactory.AbstractFabric
{
    public class Africa : Continent, IContinent
    {
        public static IContinent Create()
        {
            if (ContinentInstance == null)
            {
                lock (GateToCarnivorousInstance)
                {
                    if (ContinentInstance == null)
                    {
                        ContinentInstance = new Africa();
                    }
                }
            }
            return ContinentInstance;
        }
        public Carnivorous GetСarnivorous()
        {
            return new Carnivorous("Lion");
        }
        public Herbivorous GetHerbivorous()
        {
            return new Herbivorous("Wildebeest");
        }

        private Africa()
        {

        }
    }

    public class NorthAmerica : Continent, IContinent
    {
        public static IContinent Create()
        {
            if (ContinentInstance == null)
            {
                lock (GateToCarnivorousInstance)
                {
                    if (ContinentInstance == null)
                    {
                        ContinentInstance = new NorthAmerica();
                    }
                }
            }
            return ContinentInstance;
        }
        public Carnivorous GetСarnivorous()
        {
            return new Carnivorous("Bison");
        }
        public Herbivorous GetHerbivorous()
        {
            return new Herbivorous("Wolf");
        }

        private NorthAmerica()
        {

        }
    }

    public class Eurasia : Continent, IContinent
    {
        public static IContinent Create()
        {
            if (ContinentInstance == null)
            {
                lock (GateToCarnivorousInstance)
                {
                    if (ContinentInstance == null)
                    {
                        ContinentInstance = new Eurasia();
                    }
                }
            }
            return ContinentInstance;
        }
        public Carnivorous GetСarnivorous()
        {
            return new Carnivorous("Tiger");
        }
        public Herbivorous GetHerbivorous()
        {
            return new Herbivorous("Elk");
        }

        private Eurasia()
        {

        }
    }
}

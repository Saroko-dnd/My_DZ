using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorldAbstractFactory.AbstractFabric
{
    public interface IContinent 
    {
        Carnivorous GetСarnivorous();
        Herbivorous GetHerbivorous();
    }

    public abstract class Continent
    {
        protected static IContinent ContinentInstance;
        protected static Object GateToCarnivorousInstance = new object();
        protected Continent()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricMethodPattern
{
    public enum Airplanes : int { CargoAirplane, Airliner, SingleSeater };
    public interface IAirplane
    {
        string GetInfo();
    }
    public class CargoAirplane : IAirplane
    {
        public string GetInfo()
        {
            return "CargoAirplane";
        }
    }

    public class Airliner : IAirplane
    {
        public string GetInfo()
        {
            return "Airliner";
        }
    }

    public class SingleSeater : IAirplane
    {
        public string GetInfo()
        {
            return "SingleSeater";
        }
    }

    public abstract class AirplaneFactory
    {
        public abstract IAirplane CreateAirplane();
        protected AirplaneFactory()
        {
            
        }
    }
    public class Airlinerfactory : AirplaneFactory
    {
        public override IAirplane CreateAirplane()
        {
            return new Airliner();
        }

        public Airlinerfactory()
        {

        }
    }

    public class CargoAirlinerFactory : AirplaneFactory
    {
        public override IAirplane CreateAirplane()
        {
            return new CargoAirplane();
        }

        public CargoAirlinerFactory()
        {

        }
    }

    public class SingleSeaterFactory : AirplaneFactory
    {
        public override IAirplane CreateAirplane()
        {
            return new SingleSeater();
        }

        public SingleSeaterFactory()
        {

        }
    }
}

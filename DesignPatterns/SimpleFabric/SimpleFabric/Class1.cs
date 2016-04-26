using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFabric
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

    public class Airliner: IAirplane
    {
        public string GetInfo()
        {
            return "Airliner";
        }
    }

    public class SingleSeater: IAirplane
    {
        public string GetInfo()
        {
            return "SingleSeater";
        }
    }

    public class Airplanefactory
    {
        public static IAirplane CreateAirplane(Airplanes CurrentType)
        {
            if (CurrentType == Airplanes.Airliner)
            {
                return new CargoAirplane();
            }
            else if (CurrentType == Airplanes.CargoAirplane)
            {
                return new Airliner();
            }
            else if (CurrentType == Airplanes.SingleSeater)
            {
                return new SingleSeater();
            }
            else
            {
                return null;
            }
        }
    }

}

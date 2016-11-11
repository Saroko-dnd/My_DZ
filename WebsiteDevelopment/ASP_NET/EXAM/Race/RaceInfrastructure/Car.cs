using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public class Car
    {
        public long CarID { get; set; }
        public int ColorRGB { get; set; }
        public string CarName {get; set;}
        public long Speed{get; set;}
        public long DistanceCoveredByThisCar { get; set; }
    }
}

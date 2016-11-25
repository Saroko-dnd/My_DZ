using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure.DomainObjects
{
    public class Racer
    {
        public long RacerID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Biography { get; set; }
        public string ColorCode { get; set; }
        public string CarName { get; set; }
        public int CarSpeedKph { get; set; }
        public long DistanceCoveredInKm { get; set; }

        public Racer(string RacerFirstName, string RacerSecondName, string RacerBiography, string NewColorCode, string NewCarName, int NewCarSpeedKph)
        {
            FirstName = RacerFirstName;
            SecondName = RacerSecondName;
            Biography = RacerBiography;
            ColorCode = NewColorCode;
            CarName = NewCarName;
            CarSpeedKph = NewCarSpeedKph;
            DistanceCoveredInKm = 0;
        }

        public Racer()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public class Racer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RacerID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Biography { get; set; }
        public string ColorCode { get; set; }
        public string CarName { get; set; }
        public long CarSpeedKph { get; set; }
        public long DistanceCoveredInKm { get; set; }

        public Racer(string RacerFirstName, string RacerSecondName, string RacerBiography, string NewColorCode, string NewCarName, long NewCarSpeedKph)
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

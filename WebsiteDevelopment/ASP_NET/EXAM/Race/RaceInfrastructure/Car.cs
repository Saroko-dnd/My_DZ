using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceInfrastructure
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CarID { get; set; }
        public string ColorCode { get; set; }
        public string Name {get; set;}
        public long Speed{get; set;}
        public long DistanceCovered { get; set; }

        public Car()
        {

        }

        public Car(string NewCarColor, string NewCarName, long NewCarSpeed)
        {
            ColorCode = NewCarColor;
            Name = NewCarName;
            Speed = NewCarSpeed;
            DistanceCovered = 0;
        }
    }
}

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
        public Car SelectedCar { get; set; }
    }
}

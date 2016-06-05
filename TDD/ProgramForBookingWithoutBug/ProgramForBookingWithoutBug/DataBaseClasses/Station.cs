using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug.DataBaseClasses
{
    [Table("Stations")]
    public class Station
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StationID { get; set; }
        public string StationName { get; set; }

        public virtual ICollection<Train> Trains { get; set; }

        public Station(string NewStationName)
        {
            StationName = NewStationName;
        }

        public Station()
        {

        }
    }
}

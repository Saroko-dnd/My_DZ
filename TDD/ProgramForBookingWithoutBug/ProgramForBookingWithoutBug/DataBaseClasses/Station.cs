using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug.DataBaseClasses
{
    [Table("Stations")]
    public class Station
    {
        public string StationID { get; set; }

        public Station(string NewStationName)
        {
            StationID = NewStationName;
        }

        public Station()
        {

        }
    }
}

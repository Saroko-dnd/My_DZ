using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBookingInterfaceTest
{
    [Table("Stations")]
    public class Station
    {
        public string StationID { get; set; }

        public Station(string NewStationIDName)
        {
            StationID = NewStationIDName;
        }

        public Station()
        {

        }
    }
}

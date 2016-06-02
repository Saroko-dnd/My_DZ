using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingInterfaceTest
{
    [Table("RoutesAndFreeTickets")]
    public class RouteTicketsData
    {
        [Key]
        public int RouteID { get; set; }
        public string FirstStation { get; set; }
        public string SecondStation { get; set; }

        public int AmountOfFreeTickets { get; set; }

        public RouteTicketsData(string NewFirstStation, string NewSecondStation, int NewAmountOfFreeTickets, int NewID)
        {
            FirstStation = NewFirstStation;
            SecondStation = NewSecondStation;
            AmountOfFreeTickets = NewAmountOfFreeTickets;
            RouteID = NewID;
        }
    }
}

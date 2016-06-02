using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingInterfaceTest
{
    public class StationsDataBase : DbContext
    {
        public DbSet<RouteTicketsData> RoutesAndTickets { get; set; }
        public DbSet<Station> ListOfStations { get; set; }

        public StationsDataBase(string ConnectionStringName) : base(ConnectionStringName)
        {

        }
    }
}

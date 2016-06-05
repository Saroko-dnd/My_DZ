using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug.DataBaseClasses
{
    public class ContextForBookingDataBase : DbContext
    {
        public DbSet<Station> ListOfStations { get; set; }
        public DbSet<Train> ListOfTrains { get; set; }

        public ContextForBookingDataBase(string ConnectionStringName) : base(ConnectionStringName)
        {
            Database.SetInitializer<ContextForBookingDataBase>(new BookingDBSeeder());
        }
    }
}

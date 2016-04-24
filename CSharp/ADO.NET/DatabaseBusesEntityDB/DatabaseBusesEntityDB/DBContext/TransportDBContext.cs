using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DatabaseBusesEntityDB.DBContext
{
    public class TransportDBContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Transport> Vehicles { get; set; }
        public DbSet<TypeOfVehicle> VehicleTypes { get; set; }
        public TransportDBContext()
        {

        }

        public TransportDBContext(string ConnectionString)
            : base(ConnectionString)
        {

        }
    }
}

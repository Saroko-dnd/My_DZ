using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Examples2OneToOne
{
    public class SimpleDBContext : DbContext
    {

        public SimpleDBContext()
        {

        }

        public SimpleDBContext(string connectionString)
            : base(connectionString)
        {

        }


        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerInfo> PlayersInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Example1Simple
{
    public class SimpleDBContext:DbContext
    {

        public SimpleDBContext()
        { 
        
        }

        public SimpleDBContext(string connectionString)
            : base(connectionString)
        {

        }


       public DbSet<Player> Players { get; set; }
    }
}

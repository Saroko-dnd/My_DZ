using NewsEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDataAccess
{
    public class NewsWebsiteContext : DbContext
    {
        public NewsWebsiteContext(): base()
        {

        }

        public DbSet<News> Students { get; set; }
        public DbSet<User> Standards { get; set; }

    }
}

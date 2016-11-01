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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserOpinion> LikesAndDislikes { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }

        public NewsWebsiteContext(string ConnectionStringName): base(ConnectionStringName)
        {

        }
    }
}

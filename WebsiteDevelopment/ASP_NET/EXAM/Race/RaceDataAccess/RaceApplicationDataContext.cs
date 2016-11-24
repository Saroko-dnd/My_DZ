using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDataAccess
{
    public class RaceApplicationDataContext : DbContext
    {
        public DbSet<Racer> Racers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder ModelBuilder)
        {
            ModelBuilder.Entity<Racer>().Property(RacerEntity => RacerEntity.RacerID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ModelBuilder.Entity<User>().HasKey(x => x.UserID).Property(x => x.UserID).IsRequired();
            ModelBuilder.Entity<User>().HasMany(x => x.Roles).WithMany(x => x.Users);
            ModelBuilder.Entity<Role>().HasKey(x => x.RoleID).Property(x => x.RoleID).IsRequired();
            ModelBuilder.Entity<Role>().HasMany(x => x.Users).WithMany(x => x.Roles);
        }

        public RaceApplicationDataContext(string ConnectionStringName): base(ConnectionStringName)
        {
            Database.SetInitializer(new RaceWebsiteDBInitializer());
        }
    }
}

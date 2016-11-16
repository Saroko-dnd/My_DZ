using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceDataAccess
{
    public class RaceWebsiteDbContext : DbContext, IRaceRepository
    {
        public DbSet<Racer> Racers { get; set; }

        public IQueryable<Racer> AllRacers
        {
            get
            {
                return Racers;
            }
        }

        public void AddNewRacer(Racer NewRacer)
        {
            Racers.Add(NewRacer);
            base.SaveChanges();
        }

        public void SaveAllChanges()
        {
            base.SaveChanges();
        }

        public RaceWebsiteDbContext(string ConnectionStringName): base(ConnectionStringName)
        {
            Database.SetInitializer(new RaceWebsiteDBInitializer());
        }
    }
}

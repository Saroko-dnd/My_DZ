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
        public DbSet<Car> Cars { get; set; }
        public DbSet<Racer> Racers { get; set; }

        public IQueryable<Car> AllCars
        {
            get
            {
                return Cars;
            }
        }

        public IQueryable<Racer> AllRacers
        {
            get
            {
                return Racers;
            }
        }

        public void AddNewCar(Car NewCar)
        {
            Cars.Add(NewCar);
            base.SaveChanges();
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

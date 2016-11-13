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

        public IEnumerable<Car> AllCars
        {
            get
            {
                return Cars;
            }
            private set
            {
                throw new NotImplementedException();
            }
        }

        public void AddNewCar(Car NewCar)
        {
            Cars.Add(NewCar);
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

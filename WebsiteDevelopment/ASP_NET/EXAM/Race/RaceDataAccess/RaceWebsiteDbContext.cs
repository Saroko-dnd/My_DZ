using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Data;
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
            /*base.Database.Connection.Close();
            base.Database.Connection.Open();
            base.SaveChanges();*/
            
            using (var dbContextTransaction = base.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                try
                {
                    base.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            } 
        }

        public void UpdateRacerWithSameId(Racer UpdatedRacer)
        {
            Racers.Attach(UpdatedRacer);
            Entry(UpdatedRacer).State = EntityState.Modified;
            SaveChanges();
        }

        public RaceWebsiteDbContext(string ConnectionStringName): base(ConnectionStringName)
        {
            Database.SetInitializer(new RaceWebsiteDBInitializer());
        }
    }
}

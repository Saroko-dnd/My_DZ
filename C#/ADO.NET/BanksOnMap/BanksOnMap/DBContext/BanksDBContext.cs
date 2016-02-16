using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BanksOnMap.Entities;

namespace BanksOnMap.DBContext
{
    public class BanksDBContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<BreakTime> BreakTimes { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ExchangeRates> ExchangeRates { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }

        public BanksDBContext()
        {

        }

        public BanksDBContext(string ConnectionString)
            : base(ConnectionString)
        {

        }
    }
}

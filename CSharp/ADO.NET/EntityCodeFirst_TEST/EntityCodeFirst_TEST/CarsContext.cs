using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
//using EntityCodeFirst_TEST.Migrations;


namespace EntityCodeFirst_TEST
{
    public class CarsContext : DbContext
    {
        public DbSet<Car> Cars { get;set; }
        //static CarsContext()
        //{
           // Database.SetInitializer (new MigrateDatabaseToLatestVersion<CarsContext,Configuration>());
        //}
        public CarsContext()
        {

        }
        //Конструктор для создании базы данных с конкретной строкой подключения 
        //(или для работы с существующей БД с такой строкой подключения)
        public CarsContext( string ConnectionString) : base(ConnectionString)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityMigrationTest
{
    public class CarsSecondContext : DbContext
    {
        public DbSet<CarSecond> Cars { get; set; }

        public CarsSecondContext()
        {

        }
        //Конструктор для создании базы данных с конкретной строкой подключения 
        //(или для работы с существующей БД с такой строкой подключения)
        public CarsSecondContext(string ConnectionString)
            : base(ConnectionString)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            //демонстрация создания базы данных различными способами
            // Имя базы данных: Example1Simple.SimpleDBContext.mdf
             SimpleDBContext db = new SimpleDBContext();

          

            // Имя базы данных: SimpleDB.mdf
            // SimpleConnectionString - имя строки подключения
            // SimpleDBContext db = new SimpleDBContext("SimpleConnectionString");

            // Имя базы данных: SimpleDB.mdf
            // string ConnectionString = @"data source=(LocalDb)\v11.0;initial catalog=SimpleDB2.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            // SimpleDBContext db = new SimpleDBContext(ConnectionString);
                       
            
            Console.WriteLine("Имя БД: " + db.Database.Connection.Database);
            Console.WriteLine(db.Players.Count());


            Console.ReadKey();

        }
    }
}

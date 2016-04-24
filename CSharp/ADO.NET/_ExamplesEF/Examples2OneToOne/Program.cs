using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Examples2OneToOne
{
    class Program
    {
        static void Main(string[] args)
        {
         
             string ConnectionString = @"data source=(LocalDb)\v11.0;initial catalog=ExampleOneToOne.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
             SimpleDBContext db = new SimpleDBContext(ConnectionString);


            Console.WriteLine("Имя БД: " + db.Database.Connection.Database);

            Player player = new Player() { Age = 33, Name = "Ivan", Position = "Forvard" };
            PlayerInfo plInfo = new PlayerInfo() { Phone = "+3752955566677", Adress = "Minsk" };
            player.PlayerInfo = plInfo;
            db.Players.Add(player);
            db.SaveChanges();

            // Получение данных об игроках! 
            var Players = db.Players;
            foreach (var item in Players)
            {
                Console.WriteLine("Name:{0}, Phone:{1}, Adres:{2}", item.Name, item.PlayerInfo.Phone, item.PlayerInfo.Adress);
            }


            Console.ReadKey();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4ManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"data source=(LocalDb)\v11.0;initial catalog=ExampleManyToMany.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            SimpleDBContext db = new SimpleDBContext(ConnectionString);


            Console.WriteLine("Имя БД: " + db.Database.Connection.Database);

            Team team = new Team() { Name = "Inter", Players = new List<Player>() };
            Player player1 = new Player() { Age = 33, Name = "Ivan", Position = "Forvard" };
            team.Players.Add(player1);
            Player player2 = new Player() { Age = 33, Name = "Alex", Position = "Goalkeeper" };
            team.Players.Add(player2);
            db.Teams.Add(team);
            db.SaveChanges();

            // Получение данных об игроках! 
            foreach (var item in db.Teams)
            {
                Console.WriteLine("Name Team:{0}", item.Name);
                foreach (var pl in item.Players)
                {
                    Console.WriteLine("Name:{0}, Position: {1}", pl.Name, pl.Position);
                }
            }


            Console.ReadKey();
        }
    }
}

using Example1CodeFirst.ContextDB;
using Example1CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1CodeFirst
{
    class Program
    {   // Демонстрация Code First
        // Дополнительный пример см. 
        // в статье http://go.microsoft.com/fwlink/?LinkId=390109.
      
        static void Main(string[] args)
        {
            // Создание базы данных
            GameContext db = new GameContext();
            ShowInfoConnection(db);
            
            
            // Добавление данных в таблицу Players 
            Player pl1 = new Player() { Age = 33, Name = "Ivan", Position = "Forvard" };
            Player pl2 = new Player() { Age = 33, Name = "Alex", Position = "GoalKeeper" };
           
            // Добавление данных в таблицу Teams 
            Team team = new Team();
            team.Name = "Inter";
            team.Players = new List<Player>();
            team.Players.Add(pl1);
            team.Players.Add(pl2);
            
            db.Teams.Add(team);

            // Сохранение результатов 
            db.SaveChanges();    

            // Вывод 
            foreach (var item in db.Players)
            {
                string info = String.Format("{0} {1} {2}",item.Name, item.Age, item.Team.Name);
                Console.WriteLine(info);
            }
            

            Console.ReadKey();

        }

        static void ShowInfoConnection(GameContext db)
        {
            Console.WriteLine("Вывод строки подключения");
            Console.WriteLine(db.Database.Connection.ConnectionString);
            Console.WriteLine("Имя сервера: " + db.Database.Connection.DataSource);
            Console.WriteLine("Имя БД: " + db.Database.Connection.Database);
        
        }
    }
}

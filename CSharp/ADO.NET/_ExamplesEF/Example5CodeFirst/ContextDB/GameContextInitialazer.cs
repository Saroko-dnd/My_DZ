using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Подключение пространства имен
using System.Data.Entity;
using Example1CodeFirst.Entities;

namespace Example1CodeFirst.ContextDB
{
    public class GameContextInitialazer : DropCreateDatabaseAlways<GameContext>
    {   // переопределение метода Seed(),
        // отвечающего за инициализацию базы данных 
        // начальными значенями после создания
        protected override void Seed(GameContext context)
        {
            // Добавление данных в таблицу Players 
            Player pl1 = new Player() { Id = 1, Age = 33, Name = "Ivan", Position = "Forvard" };
            Player pl2 = new Player() { Id = 2, Age = 33, Name = "Alex", Position = "GoalKeeper" };
        
            // Добавление данных в таблицу Teams 
            Team team = new Team();
            team.Id = 1;
            team.Name = "Inter";
            team.Players = new List<Player>();
            team.Players.Add(pl1);
            team.Players.Add(pl2);
            context.Teams.Add(team);

            // Сохранение результатов 
            context.SaveChanges();

            
        }
    }
}

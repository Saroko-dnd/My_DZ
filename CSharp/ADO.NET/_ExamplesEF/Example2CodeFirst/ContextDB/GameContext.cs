using Example1CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Подключение библиотеки Entity Framework
using System.Data.Entity;

namespace Example1CodeFirst.ContextDB
{
    
    public class GameContext: DbContext
    {
        // конструтор для настройки поведения базы данных
        static GameContext()
        {
            // Создание базы данных, если он не существует (по умолчанию)
            // если существует и модель соответетствует схеме базы данных, то работаем с базой
            // если существует и модель соответетствует схеме базы данных, то работаем с базой 
            // Database.SetInitializer(new CreateDatabaseIfNotExists<GameContext>());
            
            // Удалить базу данных, если модель изменена 
                // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GameContext>());
            
            // Удаление базы и создание заново!
                // Database.SetInitializer(new DropCreateDatabaseAlways<GameContext>());
            
            // Вызов собственного инициализатора
            Database.SetInitializer(new GameContextInitialazer());

        }

        public GameContext()
            : base("GameConnectionString")
        { 
        
        }
      
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

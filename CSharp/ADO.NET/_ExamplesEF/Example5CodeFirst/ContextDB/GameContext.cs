using Example1CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Подключение библиотеки Entity Framework
using System.Data.Entity;
using Example1CodeFirst.Configuration;

namespace Example1CodeFirst.ContextDB
{
    
    public class GameContext: DbContext
    {
        // конструтор для настройки поведения базы данных
        static GameContext()
        {
            // инициализация базы данными 
            Database.SetInitializer(new GameContextInitialazer());
        }

        public GameContext()
            : base("name=GameConnectionString")
        { 
        
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // конфигурация модели с применением Fluent Api
            modelBuilder.Configurations.Add(new TeamEntityTypeConfig());
            modelBuilder.Configurations.Add(new PlayerEntityTypeConfig());
        }


      
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

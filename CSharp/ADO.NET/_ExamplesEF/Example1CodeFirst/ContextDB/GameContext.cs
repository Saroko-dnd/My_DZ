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
    // создание класса контекста, наследующего класс DbContext
    // В конструктор класса GameContext передается строка подключения к базе данных.
    // Строка подключения находится в файле конфигурации App.config
    // Имя строки подключения name=GameConnectionString
    // Если требуется выбрать другую базу данных или поставщик базы данных, 
    // измените строку подключения "GameContext" в файле конфигурации приложения.
    public class GameContext: DbContext
    {
        public GameContext()
            : base("GameConnectionString")
        { 
        
        }



        // Добавляем DbSet для каждого типа сущности, 
        // который требуется включить в модель.
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

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
    {   // Демонстрация Code First с настройкой поведения базы данных
        // изменения в классе GameContext

        // Также заполнение базы данными по умолчанию (класс GameContextInitialazer).
      
        static void Main(string[] args)
        {
            // Создание базы данных
            GameContext db = new GameContext();

            // Вывод 
            foreach (var item in db.Players)
            {
                string info = String.Format("{0} {1} {2}", item.Name, item.Age, item.Team.Name);
                Console.WriteLine(info);
            }
            

            Console.ReadKey();
        }

       
    }
}


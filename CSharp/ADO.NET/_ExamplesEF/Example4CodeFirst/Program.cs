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
        // DataAnnotations 
                        

        static void Main(string[] args)
        {
            // Создание базы данных
            GameContext db = new GameContext();
           
            // Вывод значений, содержащихся в базе данных!
            foreach (var item in db.Teams)
            {
                Console.WriteLine(item.Name);
                foreach (var pl in item.Players)
                {
                    Console.WriteLine(pl.Name + " " + pl.Position);
                }
            }

            Console.ReadKey();
        }


    }
}
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
    {   // Демонстрация Code First с миграцией данных
        // Суть миграции данных: при изменении структуры данных (добавлении нового поля, связанной таблицы т.д.) 
        // необходимо создать новую базу данных, однако при этом необходимо сохранить имеющиеся данные в БД

        //Команды для миграции 
        // Вводим команды в окне: "Консоль деспетчера пакетов"
           
        // Автоатичекие миграции
            //Enable-Migrations  
            
        // Ручные миграции
            //Add-Migration SampleMigrations 
           
            //Update-Database             - внесение информации            
            //Update-Database –Verbose     - внесение информации  (с отображение изменений)          

        static void Main(string[] args)
        {
            try
            {
                // Создание базы данных
                GameContext db = new GameContext();
                //// Добавление данных в таблицу Players 
                //Player pl1 = new Player() { Age = 33, Name = "Inna", Position = "Forvard", BirthDay=DateTime.Now };
                Player pl1 = new Player() { Age = 33, Name = "Inna", Position = "Forvard"};

                db.Players.Add(pl1);

                // Сохранение результатов 
                db.SaveChanges();

              
                    foreach (var pl in db.Players)
                    {
                        //Console.WriteLine(pl.Name + " " + pl.Position + " " + pl.BirthDay);
                        Console.WriteLine(pl.Name + " " + pl.Position);
                    }

            }
            catch(Exception ex)
            {
             Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }


    }
}
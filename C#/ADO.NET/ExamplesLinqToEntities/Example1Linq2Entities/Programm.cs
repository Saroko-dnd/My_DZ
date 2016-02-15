using DataModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1Linq2Entities
{
    public class Programm
    {

        public static void Main()
        {
            var db = new PhoneStoreEntities();
            Console.WriteLine("Простой запрос");
            //Формирование запроса на получение списка пользователей
            IQueryable<Users> query = from c in db.Users
                                      select c;
            // Вывод содержимого запроса
            Console.WriteLine(query);
            foreach (var item in query)
                Console.WriteLine(item.Login);

            Console.ReadLine();
            Console.Clear();
            
            Console.WriteLine("Запрос на выборку");
            var query2 = from c in db.Users
                        let fullName = c.Login + "  " + c.Password
                        orderby fullName
                        where c.Password.Length>3
                        select fullName;
            // Вывод содержимого запроса
            Console.WriteLine(query2);
            foreach (var item in query2)
                Console.WriteLine(item);

            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Запрос на группировку");
            var query3 = from c in db.UsersInfo
                        group c by c.Code into gr
                         select new {   Key = gr.Key, 
                                        Count = gr.Count(),
                                        Group = gr  };

            Console.WriteLine(query3);
       
            foreach (var group in query3)
            {
                if (group.Key != null)
                {
                    Console.Write("Code = {0} ,", group.Key);
                    Console.WriteLine("Count = {0}", group.Count);
                }
            }

            Console.ReadKey();

        }

    }
}

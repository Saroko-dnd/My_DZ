using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1DataBaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            // создание объекта контекста базы данных 
            PhoneStoreEntities db = new PhoneStoreEntities();
           
            // Вывод списка пользователей из таблицы Users базы данных PhoneStore
            foreach(Users user in db.Users)
            {
                Console.WriteLine("{0}.{1}, {2}", user.Id, user.Login, user.Email);
            }
          
            Console.ReadKey();
        }
    }
}

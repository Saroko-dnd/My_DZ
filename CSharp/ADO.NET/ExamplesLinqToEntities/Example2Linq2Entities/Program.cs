using DataModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2Linq2Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PhoneStoreEntities();
            Console.WriteLine("Формирование запроса");
            var query = from c in context.Users select c;
               
            Console.WriteLine("Выполнение запроса Linq2Entities");
            Console.WriteLine(query);
            foreach (var user in query)  // выполнение запроса к базе
                Console.WriteLine(user.Login);

            Console.ReadLine();
            Console.Clear();
            
            Console.WriteLine("Выполнение запроса Linq2Entities");
            int userCount = query.Count();  // выполнение запроса к базе
            Console.WriteLine("Total number of records:{0}", userCount);


            Console.WriteLine("Формирование запроса и получение данных");
            IQueryable<Users> query2 = from c in context.Users select c;
            Console.WriteLine(query2);
   
            IEnumerable<Users> Users = query.ToList(); // выполнение запроса
            // далее работаем с локальными данными (запросов к безе нет )
            foreach (var User in Users)      // обращения к базе нет
                Console.WriteLine(User.Login);
            int userCount2 = Users.Count();

            Console.WriteLine("Total number of records:{0}", userCount2); // обращения к базе нет
            Console.ReadKey();

            // Смотрим в профайлер!
        }
    }
}

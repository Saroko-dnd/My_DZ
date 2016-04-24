using DataModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example3Linq2Entities
{
    class Program
    {
        static void Main(string[] args)
        {

            var context = new PhoneStoreEntities();
            Console.WriteLine("Формирование запроса с проекцией");
            // формирование запроса 
            var query = from c in context.Users select new { c.Login, c.Password, c.Email };
            Console.WriteLine(query);
            foreach (var user in query)
                Console.WriteLine("{0} {1}", user.Login, user.Email);

            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Формирование вложенненного запроса");

            var query2 = from user in context.Users
                        orderby user.Login
                        select user;

            Console.WriteLine(query2);
            Console.WriteLine();

            var nestedQuery = from a in query2
                                where a.Password.Length < 5
                                select a;
            Console.WriteLine(nestedQuery);
            
            foreach (var user in nestedQuery)
                Console.WriteLine(user.Password);

            Console.ReadKey();

        }
    }
}

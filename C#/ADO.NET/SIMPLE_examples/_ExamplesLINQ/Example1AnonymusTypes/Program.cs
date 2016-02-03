using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1AnonymusTypes
{
    class Program
    {
        static void Main(string[] args)
        {

            // Вот так выглядит объявление анонимного типа:
            var anon = new { age = 30, name = "Ivan" };

            // дальше можно  использовать
            Console.WriteLine("age = " + anon.age + "\tname = " + anon.name);

            // вывод имени созданного типа:
            Console.WriteLine(anon.GetType());

            Console.ReadKey();
        }
    }
}

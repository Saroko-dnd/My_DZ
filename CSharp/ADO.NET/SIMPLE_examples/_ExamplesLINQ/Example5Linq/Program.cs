using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example5Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> Subjects1 = new List<String> { "C#", "WPF", "ADO.NET" };
            List<string> Subjects2 = new List<String> { "C++", "C#", "WPF" };

            Console.WriteLine("Mетод Except возвращает разность между двумя коллекциями");
            var subDiff = (from с in Subjects1 select с)
                    .Except(from c2 in Subjects2 select c2);

           //var subDiffList = Subjects1.Except(Subjects2);

            foreach (string s in subDiff)
                Console.Write(s + " ");   // Выводит ADO.NET

            Console.WriteLine("\nMетод Intersect возвращает общие элементы коллекций");
            var subCommon = (from с in Subjects1 select с)
                    .Intersect(from c2 in Subjects2 select c2);

            foreach (string s in subCommon)
                Console.Write(s+" ");   // Выводит C# и WPF


            Console.WriteLine("\nMетод Union возвращает  объединение двух коллекций");
            var subUnion = (from с in Subjects1 select с)
                    .Union(from c2 in Subjects2 select c2);

            foreach (string s in subUnion)
                Console.Write(s+" ");  // Выводит всё без повторов


            Console.WriteLine("\nMетод Concat возвращает  содержимое двух коллекций");
            var subConcat = (from с in Subjects1 select с)
                    .Concat(from c2 in Subjects2 select c2);

            foreach (string s in subConcat)
                Console.Write(s+" ");   // Выводит всё с повторами

            
            Console.ReadKey();
        }
    }
}

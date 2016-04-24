using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1Linq
{
    class Program
    {
        // Введение в запросы
        static void Main(string[] args)
        {
            // Исходный массив
            int[] array = new int[10] {5,8,7,6,2,3,4,7,8,1 };

            // получение из массива array значений > 5
            // Возвращенная последовательность хранится в переменной по имени newArray, 
            // тип которой реализует обобщенную версию интерфейса IEnumerable<T>, 
            // где T — тип System.Int32
            IEnumerable<int> newArray1 = from value in array where value > 5 select value;

            foreach (int i in newArray1)
                Console.Write(i+" ");
            
            Console.WriteLine();
            Console.WriteLine(new String('-',20));

            // получение из массива array значений > 5
            // использование неявно типизированной переменной 
            var newArray2 = from a in array where a > 5 select a;

            foreach (var i in newArray2)
                Console.Write(i + " ");

            Console.WriteLine();
            Console.WriteLine(new String('-', 20));

            // Исходный массив
            string[] Technologies = { "WinForms", "ASP.NET", "ADO.NET", "WCF", "WPF" };
            // получение из массива array значений > 5
            // использование неявно типизированной переменной 
            var newArray3 = from a in Technologies where a.StartsWith("W") select a;

            foreach (var i in newArray3)
                Console.Write(i + " ");

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2Linq
{
    class Program
    {   
        // ПРИНЦИПЫ РАБОТЫ LINQ
       
        static void Main(string[] args)
        {
            // ОТЛОЖЕННОЕ ВЫПОЛНЕНИЕ
            int[] numbers = { 10, 20, 30, 40, 1, 2, 3, 8 };
            
            // Получить числа меньше 10.
            var subset = from i in numbers where i < 10 select i;
            
            // Оператор LINQ выполняется здесь!
            foreach (var i in subset)
                Console.WriteLine("{0} < 10", i);
            Console.WriteLine();
                        
            // Изменить некоторые данные в массиве.
            numbers[0] = 4;
            // Оператор LINQ снова выполняется!
            foreach (var j in subset)
                Console.WriteLine("{0} < 10", j);
            Console.WriteLine();


            // НЕМЕДОЕННОЕ ВЫПОЛНЕНИЕ
            int[] numbers2 = { 10, 20, 30, 40, 1, 2, 3, 8 };
            // Оператор LINQ выполняется здесь!
            var subset2 = (from i in numbers2 where i < 10 select i).ToArray();

            foreach (var i in subset2)
                Console.WriteLine("{0} < 10", i);
            Console.WriteLine();

            // Изменить некоторые данные в массиве.
            numbers2[0] = 4;
          
            foreach (var j in subset2)
                Console.WriteLine("{0} < 10", j);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}

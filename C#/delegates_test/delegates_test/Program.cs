using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates_test
{
    class test_class
    {
        public delegate void my_delegate();
        my_delegate current_function;
        public void show_result()
        {
            current_function();
        }
        public test_class (my_delegate new_function)
        {
            current_function = new_function;
        }
    }
    class Program
    {
        public static void test_function()
        {
            Console.WriteLine("I DID IT!");
        }
        public delegate int do_something(int current_int);
        public delegate int do_something_witn_two_num(int int_first,int int_second);
        static public List<int> create_new_list (List<int> cur_list, do_something cur_del)
        {
            List<int> new_list = new List<int>();
            for (int counter = 0; counter < cur_list.Count(); ++counter)
            {
                new_list.Add(cur_del(cur_list[counter]));
            }
            return new_list;
        }



        static void Main(string[] args)
        {
            test_class current_test_class = new test_class(test_function);
            current_test_class.show_result();
            //Лямбда для одного аргумента
            do_something current_do_something_first = j => j + 2;
            //Лямбда для нескольких аргументов
            do_something_witn_two_num two_num_sum = (first, second) => first + second;
            do_something current_do_something = delegate (int j)
            {
                return j + 2;
            };

            List<int> random_array = new List<int>();
            List<int> new_array;
            Random cur_random = new Random();
            for (int counter = 0; counter < 30; ++counter)
            {
                random_array.Add(cur_random.Next(100));
            }
            foreach (int my_int in random_array)
            {
                current_do_something(my_int);
                Console.Write(my_int);
                Console.Write(" ");
            }
            new_array = create_new_list(random_array, current_do_something_first);
            foreach (int my_int in new_array)
            {
                Console.WriteLine(my_int);
            }
            Console.ReadKey();
        }
    }
}

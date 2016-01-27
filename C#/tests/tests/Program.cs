using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tests
{

    class test_class
    {
        public string name;
    }

    class Program
    {
        public delegate void SampleEventHandler(int new_int);
        static public event SampleEventHandler event_was_created_;

        static public event SampleEventHandler event_was_created
        {
            add
            {
                    Console.WriteLine("function added to event");
                    event_was_created_ += value;
            }
            remove
            {
                    Console.WriteLine("function was deleted from event");
                    event_was_created_ -= value;
            }
        }
        static public void test_function_ (int cur_int)
        {
            Console.WriteLine("current int" + cur_int);
        }

        static void Main()
        {
            event_was_created += test_function_;
            event_was_created_(15);
            event_was_created -= test_function_;
            test_class our_test_class = new test_class();
            our_test_class.name = "привет";
            our_test_class.name = "пока";
            our_test_class.name = "гном";
            int first = 10;
            int second = 8;
            float result_2 = (float)first / second;
            int result = first / second;
            Directory.CreateDirectory(@"test_of_dir_creation");
            Directory.CreateDirectory(@"test_of_dir_creation");
            Directory.CreateDirectory(@"test_of_dir_creation");
            Console.ReadKey();
        }
    }
}

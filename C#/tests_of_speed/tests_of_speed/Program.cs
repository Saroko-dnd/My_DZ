using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace tests_of_speed
{
    class Program
    {
        public static Random main_random = new Random();
        public static FileStream main_stream = new FileStream(@"numbers.txt",FileMode.OpenOrCreate,FileAccess.Write);
        public static StreamWriter main_writer = new StreamWriter(main_stream);
        public static Stopwatch myStopwatch = new Stopwatch();
        static void Main(string[] args)
        {
            string hhh = @"""";
            Console.WriteLine(hhh);
            myStopwatch.Start();
            int buf_number = 0;
            for (int counter = 0; counter < 1000000; ++counter )
            {
                buf_number = main_random.Next();
                if (checkForPrimality(buf_number))
                    main_writer.WriteLine(buf_number);
                if (counter % 100000 == 0)
                    Console.WriteLine(counter);
            }
            main_writer.Close();
            main_stream.Close();
            myStopwatch.Stop();
            myStopwatch.Reset();
            myStopwatch.Start();
            LinkedList<test_class> test_storage = new LinkedList<test_class>();
            for (int counter = 0; counter < 1000000; ++counter )
            {
                test_storage.AddLast(new test_class());
            }
            for (int counter = 0; counter < 200000; ++ counter )
            {
                if (main_random.Next() % 2 == 0)
                    test_storage.RemoveFirst();
                else
                    test_storage.RemoveLast();
            }
            for (int counter = 0; counter < 200000; ++counter)
            {
                if (main_random.Next() % 2 == 0)
                    test_storage.AddFirst(new test_class());
                else
                    test_storage.AddLast(new test_class());
            }
                myStopwatch.Stop();
                Console.ReadKey();
        }
        public static bool checkForPrimality(int number)
        {
            if(number > 0)
            {
                for(int i = 2; i * i <= number; i++)
                {
                    if(number % i == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        }
}


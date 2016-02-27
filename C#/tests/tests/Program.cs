using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace tests
{

    class test_class
    {
        public string name;
    }


    class Program
    {
        private static int i;
        /*static void increment(int a)
        {
            ++a;
        }

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
        internal static void Metod(){ }
        public void Foo()
        { }

        public static bool YYY ()
        {
            Console.WriteLine("YYY");
            return false;
        }

        public static bool FFF()
        {
            Console.WriteLine("FFF");
            return true;
        }

        public Program()
        {
            this.Foo();
        }*/


        static void Main()
        {
            int[,] matrix = { { 2, 4, 6, 8 }, { 2, 4, 6, 8 }, { 2, 4, 6, 8 }, { 2, 4, 6, 8 } };
            int a = matrix.GetLength(0);
            int g = matrix.Length;

            int[] l;

            int[] s = new int [3];
            string str = "" + 5;
            str = 5.ToString();
            str = "".ToString() + "5";
            //str = (Object)5.ToString();

            Func<int> d;
            d = () => 0;
            d += () => 1;
            d += () => 2;
            int b = d();
            Console.WriteLine(b);
            Console.ReadLine();
        }
    }

    /*public class ddd
    {
        public void hhh()
        {
            Console.WriteLine("hjkjghkgjh");
        }
        public ddd()
        {
            this.hhh();
        }
    }

    */public class MainClass
    {
          private readonly int number = 50;

    }

    public class TestClass
    {
        static void ghf(int d)
        {
            ++d;
        }
        static void kkk()
        {
            int e = 8;
            ghf(e);
            Console.WriteLine(e++);
        }
    }


    public interface hghgjkk
    {
        int this[string value]
        {
            get;
        }
    }

    public class fffggg : ddd
    {

    }

    sealed class ddd
    {

    }
}

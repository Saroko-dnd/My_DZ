using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritageTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadKey();
        }
    }

    public interface TestInterface
    {
        public int FirstInt;
        public int SecondInt;
    }

    public class TestClass : TestInterface
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1Lambda
{
    class Program
    {
        delegate int del(int i);
        static void Main(string[] args)
        {
            del myDelegate = x => x * x;
            int i = myDelegate(5);
            Console.WriteLine(i);
        }
    }
}

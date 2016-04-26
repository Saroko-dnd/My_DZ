using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Figures.FirstFigure TestFigure = new Figures.FirstFigure();
            Console.WriteLine(TestFigure.PrintArray());
            Console.ReadKey();
        }
    }
}

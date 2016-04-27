using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresForTetrisFactoryMethod
{
    class Program
    {
        static Random MainRandom = new Random();
        static List<Figure> AllFigures = new List<Figure>();

        static void Main(string[] args)
        {
            CreateFigures(20);
            foreach (Figure CurrentFigure in AllFigures)
            {
                Console.WriteLine(CurrentFigure.ToString());
            }
            Console.ReadKey();
        }

        static void CreateFigures(int Amount)
        {
            int RandomValue;
            for (int Counter = 0; Counter < Amount; ++Counter)
            {
                RandomValue = MainRandom.Next(1, 7);
                if (RandomValue == 1)
                {
                    AllFigures.Add(new Figures.FirstFigure());
                }
                else if(RandomValue == 2)
                {
                    AllFigures.Add(new Figures.SecondFigure());
                }
                else if (RandomValue == 3)
                {
                    AllFigures.Add(new Figures.ThirdFigure());
                }
                else if (RandomValue == 4)
                {
                    AllFigures.Add(new Figures.FourthFigure());
                }
                else if (RandomValue == 5)
                {
                    AllFigures.Add(new Figures.FifthFigure());
                }
                else if (RandomValue == 6)
                {
                    AllFigures.Add(new Figures.SixthFigure());
                }
            }
        }
    }
}

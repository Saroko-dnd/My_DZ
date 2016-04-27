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
                    AllFigures.Add(Figures.FactoryOfFirstFigures.GetFactory().CreateFigure());
                }
                else if(RandomValue == 2)
                {
                    AllFigures.Add(Figures.FactoryOfSecondFigures.GetFactory().CreateFigure());
                }
                else if (RandomValue == 3)
                {
                    AllFigures.Add(Figures.FactoryOfThirdFigures.GetFactory().CreateFigure());
                }
                else if (RandomValue == 4)
                {
                    AllFigures.Add(Figures.FactoryOfFouthFigures.GetFactory().CreateFigure());
                }
                else if (RandomValue == 5)
                {
                    AllFigures.Add(Figures.FactoryOfFifthFigures.GetFactory().CreateFigure());
                }
                else if (RandomValue == 6)
                {
                    AllFigures.Add(Figures.FactoryOfSixthFigures.GetFactory().CreateFigure());
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternAdapter
{
    class Program
    {
        public static Random MainRandom = new Random();


        static void Main(string[] args)
        {
            IUser CurrentUserInterface = new AdapterForUserInterface(new LibraryImplementation());
            Console.WriteLine("***the game has begun***");
            int RemainingBowls = 10;
            bool FirstTime = true;
            int CurrentFrame = 1;
            Console.WriteLine("Frame " + CurrentFrame.ToString());
            while (true)
            {
                int AmountOfBowls = MainRandom.Next(0, RemainingBowls + 1);
                RemainingBowls -= AmountOfBowls;
                int ThrowResult = CurrentUserInterface.ThrowBall(AmountOfBowls);
                if (ThrowResult == -1)
                {
                    break;
                }
                if ((FirstTime && AmountOfBowls == 10) || !FirstTime)
                {
                    RemainingBowls = 10;
                }
                Console.WriteLine("Player knocked " + AmountOfBowls.ToString() + " bowls Score: " + ThrowResult.ToString());
                if (AmountOfBowls == 10 && FirstTime)
                {
                    ++CurrentFrame;
                    Console.WriteLine("Frame " + CurrentFrame.ToString());
                }
                else if (AmountOfBowls < 10 && FirstTime)
                {
                    FirstTime = false;
                }
                else if (!FirstTime)
                {
                    FirstTime = true;
                    ++CurrentFrame;
                    Console.WriteLine("Frame " + CurrentFrame.ToString());
                }
            }
            Console.WriteLine("***game ended***");
            Console.ReadKey();
        }
    }
}

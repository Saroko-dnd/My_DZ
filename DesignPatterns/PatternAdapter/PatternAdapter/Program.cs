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
            int LastFrame = 0;
            while (true)
            {
                int AmountOfBowls = MainRandom.Next(0, CurrentUserInterface.GetRemainingBowls() + 1);
                int ThrowResult = CurrentUserInterface.ThrowBall(AmountOfBowls);
                if (ThrowResult == -1)
                {
                    break;
                }
                if (CurrentUserInterface.GetCurrentFrame() > LastFrame)
                {
                    LastFrame = CurrentUserInterface.GetCurrentFrame();
                    Console.WriteLine("Frame " + LastFrame.ToString());
                }
                Console.WriteLine("Player knocked " + AmountOfBowls.ToString() + " bowls Score: " + ThrowResult.ToString());
            }
            Console.WriteLine("***game ended***");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternAdapter
{
    public interface ILibrary
    {        
        int Math(int AmountOf);
    }

    public class LibraryImplementation : ILibrary
    {
        int Score = 0;
        int PrizeThrows = 0;
        int CurrentIndex = 0;
        bool FirstThrow = true;
        int[] ResultArray = new int[12];

        public int Math(int AmountOfBowls)
        {
            if (CurrentIndex >= 10 && PrizeThrows == 0)
            {
                return -1;
            }
            ResultArray[CurrentIndex] += AmountOfBowls;
            bool OneThrow = false;
            Score += AmountOfBowls;
            if (PrizeThrows > 0)
            {
                --PrizeThrows;
            }
            if (FirstThrow && AmountOfBowls == 10 && CurrentIndex == 9)
            {
                PrizeThrows = 2;
            }
            else if (!FirstThrow && CurrentIndex == 9 && ResultArray[CurrentIndex] == 10)
            {
                PrizeThrows = 1;
            }
            else if (FirstThrow && AmountOfBowls == 10)
            {
                OneThrow = true;
                ++CurrentIndex;
            }
            if (!OneThrow)
            {
                if (FirstThrow)
                {
                    FirstThrow = false;
                }
                else
                {
                    FirstThrow = true;
                    ++CurrentIndex;
                }
            }
            return Score;
        }
    }
}

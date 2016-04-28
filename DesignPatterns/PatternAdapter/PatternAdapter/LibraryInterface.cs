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
        int CurrentIndex = 0;
        bool FirstThrow = true;
        int[] ResultArray =new int[12];

        public int Math(int AmountOfBowls)
        {
            ResultArray[CurrentIndex] = AmountOfBowls;

            if (FirstThrow && AmountOfBowls == 10 && CurrentIndex == 9)
            {

            }
            else if (!FirstThrow && CurrentIndex == 9 && ResultArray[CurrentIndex] == 10)
            {

            }
            else if (FirstThrow && AmountOfBowls == 10)
            {

            }
            if (FirstThrow)
            {
                FirstThrow = false;
            }
            else
            {
                FirstThrow = true;
                ++CurrentIndex;
            }
            return 0;
        }
    }
}

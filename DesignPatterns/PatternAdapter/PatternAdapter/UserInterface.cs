using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternAdapter
{
    public interface IUser
    {
        int ThrowBall(int AmountOfBowls);
    }

    public class AdapterForUserInterface : IUser
    {
        ILibrary CurrentLibInterface;
        public int ThrowBall(int AmountOfBowls)
        {
            return CurrentLibInterface.Math(AmountOfBowls);
        }

    }
}

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
        int GetCurrentFrame();
    }

    public class AdapterForUserInterface : IUser
    {
        private ILibrary CurrentLibInterface;

        public int GetCurrentFrame()
        {
            return CurrentLibInterface.GetFrame();
        }

        public int ThrowBall(int AmountOfBowls)
        {
            return CurrentLibInterface.Math(AmountOfBowls);
        }

        public AdapterForUserInterface(ILibrary CurrentGameLibrary)
        {
            CurrentLibInterface = CurrentGameLibrary;
        }
    }
}

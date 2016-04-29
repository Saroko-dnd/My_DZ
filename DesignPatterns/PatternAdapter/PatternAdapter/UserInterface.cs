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
        int GetRemainingBowls();
    }

    public class AdapterForUserInterface : IUser
    {
        private ILibrary CurrentLibInterface;
        private int RemainingBowls = 10;
        private int CurrentFrame = 1;

        public int GetCurrentFrame()
        {
            return CurrentFrame;
        }

        public int GetRemainingBowls()
        {
            return RemainingBowls;
        }

        public int ThrowBall(int AmountOfBowls)
        {
            if (CurrentFrame < CurrentLibInterface.GetCurrentIndex() + 1)
            {
                RemainingBowls = 10;
                CurrentFrame = CurrentLibInterface.GetCurrentIndex() + 1;
            }
            return CurrentLibInterface.Math(AmountOfBowls);
        }

        public AdapterForUserInterface(ILibrary CurrentGameLibrary)
        {
            CurrentLibInterface = CurrentGameLibrary;
        }
    }
}

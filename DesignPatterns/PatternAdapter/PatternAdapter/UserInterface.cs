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
        private int currentFrameBowls = 10;
        private int LastIndexOfRound = 0;
        private ILibrary CurrentLibInterface;

        public int CurrentFrameBowls
        {
            get
            {
                return currentFrameBowls;
            }

            set
            {
                currentFrameBowls = value;
            }
        }

        public void RefreshFrame()
        {
            CurrentFrameBowls = 10;
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

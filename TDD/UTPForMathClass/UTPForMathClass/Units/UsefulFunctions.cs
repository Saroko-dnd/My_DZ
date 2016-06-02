using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTPForMathClass
{
    public static class UsefulFunctions
    {
        public static double RaiseDoubleToDegree(double CurrentDouble, int Degree)
        {
            double CopyOfDouble = CurrentDouble;
            for (int Counter = 1; Counter < Degree; ++Counter)
            {
                CurrentDouble *= CopyOfDouble;
            }

            return CurrentDouble;
        }

        public static double Factorial(byte NewNumber)
        {
            ulong Result = 1;
            for (byte Counter = 2; Counter <= NewNumber; ++Counter)
            {
                Result *= Counter;
            }
            return (double)Result;
        }
    }
}

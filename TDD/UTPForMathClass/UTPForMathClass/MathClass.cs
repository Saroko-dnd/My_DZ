using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTPForMathClass
{
    public class MathClass
    {
        static public double Cos(string number)
        {
            double CurrentNumber = double.Parse(number);
            if (CurrentNumber > (2*Math.PI))
            {
                CurrentNumber = CurrentNumber % (2 * Math.PI);
                if (CurrentNumber > Math.PI)
                {

                }
            }
            double Result = 1.0;

            bool MinusNow = true;
            for (int Counter = 2; Counter < 20; Counter += 2 )
            {
                if (MinusNow)
                {
                    Result -= (RaisedToThePower(CurrentNumber, Counter) / Factorial(Counter));
                    MinusNow = false;
                }
                else
                {
                    Result += (RaisedToThePower(CurrentNumber, Counter) / Factorial(Counter));
                    MinusNow = true;
                }
            }

            return Result;
        }

        public static double RaisedToThePower(double CurrentDouble, int Degree)
        {
            double CopyOfDouble = CurrentDouble;
            for (int Counter = 1; Counter < Degree; ++Counter)
            {
                CurrentDouble *= CopyOfDouble;
            }

            return CurrentDouble;
        }

        public static double Factorial(int NewNumber)
        {
            int Result = 1;
            for (int Counter = 2; Counter <= NewNumber; ++Counter)
            {
                Result *= Counter;
            }
            return (double)Result;
        }
    }
}

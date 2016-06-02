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
            CurrentNumber = CurrentNumber % (2 * Math.PI);

            bool Minus;
            bool NoChangesForSign = true;
            if (CurrentNumber > 0)
            {
                Minus = false;
                while (CurrentNumber > Math.PI / 2)
                {
                    CurrentNumber -= Math.PI;
                    if (Minus)
                    {
                        Minus = false;
                    }
                    else
                    {
                        Minus = true;
                    }
                    NoChangesForSign = false;
                }
            }
            else
            {
                Minus = true;
                while (CurrentNumber < -(Math.PI / 2))
                {
                    CurrentNumber += Math.PI;
                    if (Minus)
                    {
                        Minus = false;
                    }
                    else
                    {
                        Minus = true;
                    }
                    NoChangesForSign = false;
                }
                if (!Minus)
                {
                    Minus = true;
                }
            }

            double Result = 1.0;

            bool MinusNow = true;
            for (byte Counter = 2; Counter < 20; Counter += 2 )
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
            if (Minus && !NoChangesForSign)
            {
                Result = -Result;
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

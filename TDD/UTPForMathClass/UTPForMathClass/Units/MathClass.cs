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
            Minus = BringingDoubleToCalculatedValueForCosMethod(ref CurrentNumber, ref NoChangesForSign);

            double Result = 1.0;

            bool MinusNow = true;
            for (byte Counter = 2; Counter < 20; Counter += 2)
            {
                if (MinusNow)
                {
                    Result -= (UsefulFunctions.RaiseDoubleToDegree(CurrentNumber, Counter) / UsefulFunctions.Factorial(Counter));
                    MinusNow = false;
                }
                else
                {
                    Result += (UsefulFunctions.RaiseDoubleToDegree(CurrentNumber, Counter) / UsefulFunctions.Factorial(Counter));
                    MinusNow = true;
                }
            }
            if (Minus && !NoChangesForSign)
            {
                Result = -Result;
            }
            return Result;
        }

        /// <summary>
        /// Изменяет double переменную таким образом, чтобы в дальнейшем к ней можно было применить формулу вычисления косинуса угла (указанного в радианах)
        /// и возвращает bool переменную, которая показывает нужно ли изменить знак результата приминения формулы на противоположный
        /// </summary>
        /// <param name="CurrentNumber"></param>
        /// <param name="NoChangesForSign"></param>
        /// <returns></returns>
        private static bool BringingDoubleToCalculatedValueForCosMethod(ref double CurrentNumber, ref bool NoChangesForSign)
        {
            bool Minus;
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

            return Minus;
        }
        
    }
}

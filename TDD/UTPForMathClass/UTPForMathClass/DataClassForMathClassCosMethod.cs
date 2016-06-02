using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTPForMathClass
{
    public class DataClassForMathClassCosMethod
    {
        private bool minus;
        private bool noChangesForSign;
        private double CurrentAngleInRadians;

        public bool Minus
        {
            get
            {
                return minus;
            }

            set
            {
                minus = value;
            }
        }

        public bool NoChangesForSign
        {
            get
            {
                return noChangesForSign;
            }

            set
            {
                noChangesForSign = value;
            }
        }

        public DataClassForMathClassCosMethod(bool MinusValue, bool NoChangesForSignValue, double NewAngleInRadians)
        {
            Minus = MinusValue;
            NoChangesForSign = NoChangesForSignValue;
            CurrentAngleInRadians = NewAngleInRadians;
        }
    }
}

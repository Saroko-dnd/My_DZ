using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLINQexample
{
    public static class BinaryOperators
    {
        public static readonly BinaryOperator GreaterThan = new BinaryOperator(">");
        public static readonly BinaryOperator LessThan = new BinaryOperator("<");
        public static readonly BinaryOperator Equal = new BinaryOperator("==");
        public static readonly BinaryOperator GreaterThanOrEqual = new BinaryOperator(">=");
        public static readonly BinaryOperator LessThanOrEqual = new BinaryOperator("<=");
    }
}

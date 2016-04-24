using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLINQexample
{
    public static class BinaryOperators
    {
        public static readonly BinaryOperator GreaterThan = new BinaryOperator(MyResourses.BinaryOperatorsValues.GreaterThan);
        public static readonly BinaryOperator LessThan = new BinaryOperator(MyResourses.BinaryOperatorsValues.LessThan);
        public static readonly BinaryOperator Equal = new BinaryOperator(MyResourses.BinaryOperatorsValues.Equal);
        public static readonly BinaryOperator GreaterThanOrEqual = new BinaryOperator(MyResourses.BinaryOperatorsValues.GreaterThanOrEqual);
        public static readonly BinaryOperator LessThanOrEqual = new BinaryOperator(MyResourses.BinaryOperatorsValues.LessThanOrEqual);
        public static readonly BinaryOperator NotEqual = new BinaryOperator(MyResourses.BinaryOperatorsValues.NotEqual);
    }
}

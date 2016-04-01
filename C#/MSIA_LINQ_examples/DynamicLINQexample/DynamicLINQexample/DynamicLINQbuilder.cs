using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicLINQexample
{
    public static class DynamicLINQbuilder
    {
        public static Func<Tin, bool> WhereMethod<Tin>(string PropertyName,object Constant)
        {
                ParameterExpression VariableResultParam = Expression.Variable(typeof(Tin));
                ConstantExpression SecondConstant = Expression.Constant(Constant);
                BinaryExpression CurrentBinaryExpression = Expression.GreaterThan(VariableResultParam, SecondConstant);
                return Expression<Func<Tin, bool>>.Lambda<Func<Tin, bool>>(CurrentBinaryExpression, VariableResultParam).Compile();
        }
    }
}

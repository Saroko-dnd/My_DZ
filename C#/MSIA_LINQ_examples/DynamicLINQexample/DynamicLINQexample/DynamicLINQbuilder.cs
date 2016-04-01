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
            ConstantExpression RightConstant = Expression.Constant(Constant);
            if (PropertyName == null)
            {
                ParameterExpression LeftVariable = Expression.Variable(typeof(Tin));
                BinaryExpression CurrentBinaryExpression = Expression.GreaterThan(LeftVariable, RightConstant);
                return Expression<Func<Tin, bool>>.Lambda<Func<Tin, bool>>(CurrentBinaryExpression, LeftVariable).Compile();
            }
            else
            {
                ParameterExpression LeftObject = Expression.Parameter(typeof(Tin));
                MemberInfo LeftPropertyInfo = typeof(Tin).GetProperty(PropertyName);
                MemberExpression LeftProperty = Expression.MakeMemberAccess(LeftObject, LeftPropertyInfo);
                BinaryExpression CurrentBinaryExpression = Expression.GreaterThan(LeftProperty, RightConstant);
                return Expression<Func<Tin, bool>>.Lambda<Func<Tin, bool>>(CurrentBinaryExpression, LeftObject).Compile();
            }
        }
    }
}

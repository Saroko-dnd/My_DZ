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
        public static Func<Tin, bool> WhereMethod<Tin>(string PropertyName,object Constant, BinaryOperator BinaryOperator)
        {
            ConstantExpression RightConstant = Expression.Constant(Constant);
            if (PropertyName == null)
            {
                ParameterExpression LeftVariable = Expression.Variable(typeof(Tin));
                BinaryExpression CurrentBinaryExpression = null;
                if (BinaryOperator.Operator == ">")
                {
                    CurrentBinaryExpression = Expression.GreaterThan(LeftVariable, RightConstant);
                }
                else if (BinaryOperator.Operator == "<")
                {
                    CurrentBinaryExpression = Expression.LessThan(LeftVariable, RightConstant);
                }
                else if (BinaryOperator.Operator == "==")
                {
                    CurrentBinaryExpression = Expression.Equal(LeftVariable, RightConstant);
                }
                else if (BinaryOperator.Operator == ">=")
                {
                    CurrentBinaryExpression = Expression.GreaterThanOrEqual(LeftVariable, RightConstant);
                }
                else if (BinaryOperator.Operator == "<=")
                {
                    CurrentBinaryExpression = Expression.LessThanOrEqual(LeftVariable, RightConstant);
                }
                return Expression<Func<Tin, bool>>.Lambda<Func<Tin, bool>>(CurrentBinaryExpression, LeftVariable).Compile();
            }
            else
            {
                ParameterExpression LeftObject = Expression.Parameter(typeof(Tin));
                MemberInfo LeftPropertyInfo = typeof(Tin).GetProperty(PropertyName);
                MemberExpression LeftProperty = Expression.MakeMemberAccess(LeftObject, LeftPropertyInfo);
                BinaryExpression CurrentBinaryExpression = null;
                if (BinaryOperator.Operator == ">")
                {
                    CurrentBinaryExpression = Expression.GreaterThan(LeftProperty, RightConstant);
                }
                else if (BinaryOperator.Operator == "<")
                {
                    CurrentBinaryExpression = Expression.LessThan(LeftProperty, RightConstant);
                }
                else if (BinaryOperator.Operator == "==")
                {
                    CurrentBinaryExpression = Expression.Equal(LeftProperty, RightConstant);
                }
                else if (BinaryOperator.Operator == ">=")
                {
                    CurrentBinaryExpression = Expression.GreaterThanOrEqual(LeftProperty, RightConstant);
                }
                else if (BinaryOperator.Operator == "<=")
                {
                    CurrentBinaryExpression = Expression.LessThanOrEqual(LeftProperty, RightConstant);
                }
                return Expression<Func<Tin, bool>>.Lambda<Func<Tin, bool>>(CurrentBinaryExpression, LeftObject).Compile();
            }
        }
    }
}

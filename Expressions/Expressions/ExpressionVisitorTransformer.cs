using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Expressions
{
    public class ExpressionVisitorTransformer : ExpressionVisitor
    {
        private Dictionary<string, object> exchangeParameters;
        public ExpressionVisitorTransformer() { }

        public ExpressionVisitorTransformer(Dictionary<string, object> exchangeParameters) : base()
        {
            this.exchangeParameters = exchangeParameters;
        }


        private bool IsIntegerParameterType(Expression node)
        {
            return (node.NodeType == ExpressionType.Parameter && node.Type == typeof(int));
        }

        private bool IsConstantOneParameterType(Expression node)
        {
            var expr = node as ConstantExpression;
            if(expr != null && expr.Type == typeof(int))
            {
                return (int)expr.Value == 1;
            }
            return false;
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            object val;
            if(!exchangeParameters.TryGetValue(parameter.Name, out val))
            {
                return base.VisitParameter(parameter);
            }
            return Expression.Constant(val, parameter.Type);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Expression.Lambda(Visit(node.Body), node.Name, node.Parameters.Where(parameterExpression => !exchangeParameters.ContainsKey(parameterExpression.Name)));
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (IsIntegerParameterType(node.Left) && IsConstantOneParameterType(node.Right))
            {
                if (node.NodeType == ExpressionType.Add)
                    return Expression.Increment(node.Left);
                if (node.NodeType == ExpressionType.Subtract)
                    return Expression.Decrement(node.Left);
            }
            var left = Visit(node.Left);
            var right = Visit(node.Right);
            return Expression.MakeBinary(node.NodeType, left, right);
        }
    }
}

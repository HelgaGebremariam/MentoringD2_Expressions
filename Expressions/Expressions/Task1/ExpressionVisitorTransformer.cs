using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Expressions.Task1
{
    public class ExpressionVisitorTransformer : ExpressionVisitor
    {
        private Dictionary<string, object> exchangeParameters;
        public ExpressionVisitorTransformer() { }

        public ExpressionVisitorTransformer(Dictionary<string, object> exchangeParameters) : base()
        {
            this.exchangeParameters = exchangeParameters;
        }


        private bool IsParameterHasType<T>(Expression node)
        {
            return (node.NodeType == ExpressionType.Parameter && node.Type == typeof(T));
        }

        private bool IsParameterEqualValue(Expression node, int value)
        {
            var expr = node as ConstantExpression;
            if(expr != null && expr.Type == typeof(int))
            {
                return (int)expr.Value == value;
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
            if (IsParameterHasType<int>(node.Left) && IsParameterEqualValue(node.Right, 1) && !exchangeParameters.Any(s=>s.Key == ((ParameterExpression)node.Left).Name))
            {
                if (node.NodeType == ExpressionType.Add)
                    return Expression.Increment(node.Left);
                if (node.NodeType == ExpressionType.Subtract)
                    return Expression.Decrement(node.Left);
            }
            return base.VisitBinary(node);
        }
    }
}

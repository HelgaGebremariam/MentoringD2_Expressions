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
        private IEnumerable<KeyValuePair<string, object>> exchangeParameters;
        public ExpressionVisitorTransformer() { }

        public ExpressionVisitorTransformer(IEnumerable<KeyValuePair<string, object>> exchangeParameters) : base()
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
            var exchangeParameter = exchangeParameters.Where(p => p.Key == parameter.Name).FirstOrDefault();
            if(exchangeParameter.Value == null)
            {
                return base.VisitParameter(parameter);
            }
            return Expression.Constant(exchangeParameter.Value);

        }

        //protected override Expression VisitBinary(BinaryExpression node)
        //{
        //    if (IsIntegerParameterType(node.Left) && IsConstantOneParameterType(node.Right))
        //    {
        //        if (node.NodeType == ExpressionType.Add)
        //            return Expression.Increment(node.Left);
        //        if (node.NodeType == ExpressionType.Subtract)
        //            return Expression.Decrement(node.Left);
        //    }
        //    return base.Visit(node);
        //}
    }
}

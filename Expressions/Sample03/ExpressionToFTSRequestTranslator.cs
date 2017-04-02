using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample03
{
	public class ExpressionToFTSRequestTranslator : ExpressionVisitor
	{
		List<StringBuilder> resultStrings;

		public List<string> Translate(Expression exp)
		{
			resultStrings = new List<StringBuilder>();
            resultStrings.Add(new StringBuilder());
			Visit(exp);

            return resultStrings.Select(r => r.ToString()).ToList();
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node.Method.DeclaringType == typeof(Queryable)
				&& node.Method.Name == "Where")
			{
				var predicate = node.Arguments[1];
				Visit(predicate);

				return node;
			}

            if (node.Method.DeclaringType == typeof(string)
                && node.Method.Name == "Contains")
            {
                Visit(node.Object);
                resultStrings.Last().Append("(*");
                Visit(node.Arguments[0]);
                resultStrings.Last().Append("*)");
                return node;
            }

            if (node.Method.DeclaringType == typeof(string)
                && node.Method.Name == "StartsWith")
            {
                Visit(node.Object);
                resultStrings.Last().Append("(");
                Visit(node.Arguments[0]);
                resultStrings.Last().Append("*)");
                return node;
            }

            if (node.Method.DeclaringType == typeof(string)
                && node.Method.Name == "EndsWith")
            {
                Visit(node.Object);
                resultStrings.Last().Append("(*");
                Visit(node.Arguments[0]);
                resultStrings.Last().Append(")");
                return node;
            }

            return base.VisitMethodCall(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			switch (node.NodeType)
			{
                case ExpressionType.AndAlso:
                    {
                        Visit(node.Left);
                        resultStrings.Add(new StringBuilder());
                        Visit(node.Right);
                        break;
                    }
				case ExpressionType.Equal:
					if ((node.Left.NodeType == ExpressionType.MemberAccess) && (node.Right.NodeType == ExpressionType.Constant))
                    {
                        Visit(node.Left);
                        resultStrings.Last().Append("(");
                        Visit(node.Right);
                        resultStrings.Last().Append(")");
                        break;
                    }

                    if ((node.Right.NodeType == ExpressionType.MemberAccess) && (node.Left.NodeType == ExpressionType.Constant))
                    {
                        Visit(node.Right);
                        resultStrings.Last().Append("(");
                        Visit(node.Left);
                        resultStrings.Last().Append(")");
                        break;
                    }
						throw new NotSupportedException(string.Format("Right operand should be constant", node.NodeType));
				default:
					throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
			};

			return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
            resultStrings.Last().Append(node.Member.Name).Append(":");

			return base.VisitMember(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
            resultStrings.Last().Append(node.Value);

			return node;
		}
	}
}

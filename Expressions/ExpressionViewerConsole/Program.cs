using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expressions;
using System.Linq.Expressions;

namespace ExpressionViewerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int>> source_exp = (c) => c - 1 + 2 * c;
            var exchangeRules = new Dictionary<string, object>();
            exchangeRules.Add("c", 1);
            ExpressionVisitorTransformer visitor = new ExpressionVisitorTransformer(exchangeRules);
            Expression expr = source_exp;
            visitor.VisitAndConvert(expr, "");

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expressions.Task1;
using System.Linq.Expressions;

namespace ExpressionViewerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int, int>> source_exp = (a, c) => c - 1 + 2 * a;
            var exchangeRules = new Dictionary<string, object>();
            exchangeRules.Add("c", 13);
            ExpressionVisitorTransformer visitor = new ExpressionVisitorTransformer(exchangeRules);
            var expr = visitor.Visit(source_exp);

        }
    }
}

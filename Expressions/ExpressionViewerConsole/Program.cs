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
            var exchangeRules = new List<KeyValuePair<string, object>>();
            exchangeRules.Add(new KeyValuePair<string, object>("c", 1));
            ExpressionVisitorTransformer visitor = new ExpressionVisitorTransformer(exchangeRules);
            visitor.VisitAndConvert(source_exp, "");

        }
    }
}

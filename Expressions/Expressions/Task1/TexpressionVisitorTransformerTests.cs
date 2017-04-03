using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Expressions.Task1
{
    [TestClass]
    public class TexpressionVisitorTransformerTests
    {
        [TestMethod]
        public void TestVisitorTransformer_IsValidResults()
        {
            Expression<Func<int, int, int>> source_exp = (a, c) => c - 1 + 2 * a;
            var exchangeRules = new Dictionary<string, object>();
            exchangeRules.Add("c", 13);
            ExpressionVisitorTransformer visitor = new ExpressionVisitorTransformer(exchangeRules);
            Expression expr = visitor.Visit(source_exp);
            Expression<Func<int, int>> expected = (a) => 13 - 1 + 2 * a;

            int expectedReturn = expected.Compile().Invoke(2);
            int actualReturn = ((Expression<Func<int, int>>)expr).Compile().Invoke(2);

            Assert.AreEqual(expectedReturn, actualReturn);
        }
    }
}

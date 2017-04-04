using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Expressions.Task2
{
	public class TestClass1
	{
		public int a { get; set; }
		public string b { get; set; }
		public DateTime c { get; set; }
	}

	public class TestClass2
	{
		public int a { get; set; }
		public string b { get; set; }
		public DateTime c { get; set; }
	}

	[TestClass]
	public class MappingGeneratorTests
	{
		[TestMethod]
		public void TestMappingGenerator_IsValidResults()
		{
			MappingGenerator generator = new MappingGenerator();
			Mapper<TestClass1, TestClass2> mapper = generator.Generate<TestClass1, TestClass2>();
			var testClass1 = new TestClass1() { a = 10, b = "str", c = DateTime.Now };
			var testClass2 = mapper.Map(testClass1);

			Assert.AreEqual(testClass2.a, testClass1.a);
			Assert.AreEqual(testClass2.b, testClass1.b);
			Assert.AreEqual(testClass2.c, testClass1.c);
		}
	}
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample03.E3SClient.Entities;
using Sample03.E3SClient;
using System.Configuration;
using System.Linq;

namespace Sample03
{
	[TestClass]
	public class E3SProviderTests
	{
		[TestMethod]
		public void WithoutProvider()
		{
			var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"] , ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS<EmployeeEntity>(0, 1, "workstation:(EPRUIZHW0249)");

            foreach (var emp in res)
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

		[TestMethod]
		public void WithoutProviderNonGeneric()
		{
			var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS(typeof(EmployeeEntity), 0, 10, "workstation:(EPRUIZHW0249)");

            foreach (var emp in res.OfType<EmployeeEntity>())
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }


		[TestMethod]
		public void WithProvider()
		{
			var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation == "EPRUIZHW0249"))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }


        }

        [TestMethod]
        public void StartsWithTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.StartsWith("EPRUIZHW0249")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [TestMethod]
        public void EndsWithTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.EndsWith("EPRUIZHW0249")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [TestMethod]
        public void ContainsTest()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.Contains("EPRUIZHW0249") && e.workstation.Contains("EPRUIZHW0249") && e.workstation.Contains("EPRUIZHW0249")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }
    }
}

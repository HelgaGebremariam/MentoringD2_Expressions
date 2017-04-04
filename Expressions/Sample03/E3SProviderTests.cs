using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample03.E3SClient.Entities;
using Sample03.E3SClient;
using System.Configuration;
using System.Linq;
using Moq;

namespace Sample03
{
	[TestClass]
	public class E3SProviderTests
	{

        [TestMethod]
        public void StartsWithTest()
        {
			var e3sQueryClientMock = new Mock<E3SQueryClient>();
			e3sQueryClientMock.Setup(x => x.SearchFTS(It.IsAny<Type>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string[]>()));
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

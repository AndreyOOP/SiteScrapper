using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Controllers;

namespace ParserApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            ValuesController controller = new ValuesController();

            IEnumerable<string> result = controller.Get();

            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            ValuesController controller = new ValuesController();

            TestModel result = controller.Get(5);

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(2, result.Id2);
        }
    }
}

using CarPartsParser.ContainerSetup;
using CarPartsParser.Models;
using CarPartsParser.SiteParsers.RootParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace CarPartsParserPrototypeTests
{
    [TestClass]
    public class RootParserTests
    {
        [TestMethod]
        public void ParseTest()
        {
            var containerProvider = new ContainerProvider();
            var container = containerProvider.Get();
            var rootParser = container.Resolve<ParserExecutor>();
            
            var result = rootParser.Parse(new In { Id = "123" }).ToList();
            
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(r => r.GetType() == typeof(ParserExecutorResultBase)));

            var aResult = result.ToArray()[0];
            Assert.AreEqual(null, aResult.Exception);
            Assert.AreEqual("WorkUnit_1_A set Prop1; Execute Service A", aResult.Prop1);
            Assert.AreEqual("WorkUnit_2_A set Prop2; Execute Service B", aResult.Prop2);

            var bResult = result.ToArray()[1];
            Assert.AreEqual(null, bResult.Exception);
            Assert.AreEqual("WorkUnit_1_B set Prop1", bResult.Prop1);
            Assert.AreEqual(null, bResult.Prop2);
        }
    }
}

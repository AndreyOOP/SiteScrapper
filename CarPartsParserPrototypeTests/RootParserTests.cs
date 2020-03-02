using CarPartsParser.ContainerSetup;
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
            var rootParser = container.Resolve<RootParser>();

            var result = rootParser.Parse("").ToList();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(r => r.GetType() == typeof(RootResult)));
        }
    }
}

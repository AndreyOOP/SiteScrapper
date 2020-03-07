using CarPartsParser.Abstraction;
using CarPartsParser.ContainerSetup;
using CarPartsParser.Models;
using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.SiteParsers.RootParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
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
            Assert.IsTrue(result.Any(r => r.GetType() == typeof(ParserExecutorResult)));
        }
    }
}

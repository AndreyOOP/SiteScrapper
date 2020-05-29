using CarPartsParser.Abstraction.Models;
using CarPartsParser.Models;
using CarPartsParser.Parser.Tree;
using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.SiteParsers.Abstraction.WorkUnits;
using CarPartsParserPrototypeTests.WorkUnitTreeTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace CarPartsParserPrototypeTests
{
    [TestClass]
    public class WebSiteParserTests
    {
        [TestMethod]
        public void Parse_WorkUnitThrowsException_SaveToExceptionProperty()
        {
            var tree = new WorkUnitTree(new WuRoot());
            tree.AddNextNode(new WorkUnitTree(new Wu1()))
                .AddNextNode(new WorkUnitTree(new WorkUnitException()))
                .AddNextNode(new WorkUnitTree(new Wu2()));

            var parser = new WebSiteParser<IWorkUnit, ParserExecutorResult>(tree);

            var result = parser.Parse(new In { Id = "1" });

            var expected = JsonConvert.SerializeObject(new { 
                UnitName = "WorkUnitException", 
                Message = "Exception in work unit", 
                Path = "WuRoot > Wu1 > WorkUnitException > " 
            });
            Assert.AreEqual(expected, result.Exception);
        }
    }

    class WorkUnitException : IWorkUnit
    {
        public IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            throw new Exception("Exception in work unit");
        }
    }
}

using CarPartsParser.Models;
using CarPartsParser.Parser.Tree;
using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.SiteParsers.Abstraction.WorkUnits;
using CarPartsParserPrototypeTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarPartsParserPrototypeTests.WorkUnitTreeTests
{
    [TestClass]
    public class PathPropertyTest
    {
        private WebSiteParser<IWorkUnit, ParserExecutorResult> parser;

        [TestInitialize]
        public void Initialize()
        {
            var tree = new WorkUnitTree(new WuRoot());
            var paths = tree.AddConditionalNodes(TestWorkUnit.PathSelector, new Dictionary<ExecutionPath, WorkUnitTree>
            {
                [ExecutionPath.Path1] = new WorkUnitTree(new Wu1()),
                [ExecutionPath.Path2] = new WorkUnitTree(new Wu2()),
                [ExecutionPath.Path3] = new WorkUnitTree(new Wu3()),
            });
            paths[ExecutionPath.Path2].AddNextNode(new WorkUnitTree(new WuA()))
                                      .AddNextNode(new WorkUnitTree(new WuB()));

            parser = new WebSiteParser<IWorkUnit, ParserExecutorResult>(tree);
        }

        [TestMethod]
        [DataRow(ExecutionPath.Path1, "WuRoot > Wu1 > ")]
        [DataRow(ExecutionPath.Path2, "WuRoot > Wu2 > WuA > WuB > ")]
        [DataRow(ExecutionPath.Path3, "WuRoot > Wu3 > ")]
        public void Parse_ExecutionPaths_CorrectUnitsPassed(ExecutionPath path, string expected)
        {
            var result = parser.Parse(new TestWorkUnitModel(path));

            Assert.AreEqual(expected, result.ExecutionPath);
        }
    }
}

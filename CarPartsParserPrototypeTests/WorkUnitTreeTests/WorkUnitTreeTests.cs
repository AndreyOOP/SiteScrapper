using CarPartsParser.Parser.Tree;
using CarPartsParserPrototypeTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CarPartsParserPrototypeTests.WorkUnitTreeTests
{
    [TestClass]
    public class WorkUnitTreeTests
    {
        [TestMethod]
        [DataRow(ExecutionPath.Path1, "Wu1")]
        [DataRow(ExecutionPath.Path2, "Wu2")]
        [DataRow(ExecutionPath.Path3, "Wu3")]
        public void NextNode_CorrectInputModel_Ok(ExecutionPath path, string expectedTypeName)
        {
            var tree = new WorkUnitTree(new WuRoot());
            tree.AddConditionalNodes(TestWorkUnit.PathSelector, new Dictionary<ExecutionPath, WorkUnitTree>
            {
                [ExecutionPath.Path1] = new WorkUnitTree(new Wu1()),
                [ExecutionPath.Path2] = new WorkUnitTree(new Wu2()),
                [ExecutionPath.Path3] = new WorkUnitTree(new Wu3())
            });

            var actualNode = tree.NextNode(new TestWorkUnitModel(path), null);

            Assert.AreEqual(expectedTypeName, actualNode.Unit.GetType().Name);
        }

        [TestMethod]
        public void NextNode_UnexpectedInputModel_KeyNotFoundException()
        {
            var tree = new WorkUnitTree(new WuRoot());
            tree.AddConditionalNodes(TestWorkUnit.PathSelector, new Dictionary<ExecutionPath, WorkUnitTree>
            {
                [ExecutionPath.Path1] = new WorkUnitTree(new Wu1()),
                [ExecutionPath.Path2] = new WorkUnitTree(new Wu2()),
                [ExecutionPath.Path3] = new WorkUnitTree(new Wu3())
            });

            Assert.ThrowsException<KeyNotFoundException>
            (
                () => tree.NextNode(new TestWorkUnitModel(ExecutionPath.Path4), null)
            );
        }

        [TestMethod]
        public void NextNode_NodeLastInChain_InvalidOperationException()
        {
            var tree = new WorkUnitTree(new WuRoot());

            Assert.ThrowsException<InvalidOperationException>
            (
                () => tree.NextNode(new TestWorkUnitModel(ExecutionPath.Path1), null)
            );
        }

        class WuRoot : WorkUnitTestBase
        {
            public WuRoot(TestWorkUnitModel workUnit = null) : base(workUnit) { }
        }
        class Wu1 : WorkUnitTestBase
        {
            public Wu1(TestWorkUnitModel workUnit = null) : base(workUnit) { }
        }
        class Wu2 : WorkUnitTestBase
        {
            public Wu2(TestWorkUnitModel workUnit = null) : base(workUnit) { }
        }
        class Wu3 : WorkUnitTestBase
        {
            public Wu3(TestWorkUnitModel workUnit = null) : base(workUnit) { }
        }
    }
}

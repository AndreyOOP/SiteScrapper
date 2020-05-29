using CarPartsParser.Parser.Tree;
using CarPartsParser.Parser.WorkUnitsSiteA;
using CarPartsParserPrototypeTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CarPartsParserPrototypeTests.WorkUnitTreeTests
{
    [TestClass]
    public class DrawWorkUnitTree
    {
        // Required for graphical representation of the builded tree
        // it is just sample so all units are WorkUnit1A in fact they should to be different
        [TestMethod]
        public void DrawTree_SamplesOfTreesDisplayed()
        {
            var u = new WorkUnit1A(null);

            var tree = new WorkUnitTree(u);
            var paths1 = tree.AddNextNode(new WorkUnitTree(u))
                             .AddConditionalNodes((i, o) => ExecutionPath.Path1, new Dictionary<ExecutionPath, WorkUnitTree>
                             {
                                  [ExecutionPath.Path1] = new WorkUnitTree(u),
                                  [ExecutionPath.Path2] = new WorkUnitTree(u),
                                  [ExecutionPath.Path3] = new WorkUnitTree(u)
                             });

            paths1[ExecutionPath.Path1].AddNextNode(new WorkUnitTree(u));
            var path2 = paths1[ExecutionPath.Path2].AddConditionalNodes((i, o) => ExecutionPath.Path2, new Dictionary<ExecutionPath, WorkUnitTree>
            {
                [ExecutionPath.Path1] = new WorkUnitTree(u),
                [ExecutionPath.Path2] = new WorkUnitTree(u)
            });
            path2[ExecutionPath.Path1].AddNextNode(new WorkUnitTree(u)).AddNextNode(new WorkUnitTree(u)).AddNextNode(new WorkUnitTree(u));
            path2[ExecutionPath.Path2].AddConditionalNodes((i, o) => ExecutionPath.Path1, new Dictionary<ExecutionPath, WorkUnitTree>
            {
                [ExecutionPath.Path1] = new WorkUnitTree(u),
                [ExecutionPath.Path2] = new WorkUnitTree(u)
            });

            Console.WriteLine("Complete tree:");
            Console.WriteLine(tree.DrawTree(""));

            Console.WriteLine();
            Console.WriteLine("Execution path: Path1->Path2");
            Console.WriteLine(paths1[ExecutionPath.Path2].DrawTree(""));
        }

        [TestMethod]
        public void DrawTree_DifferentWorkUnitsUsed()
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

            Console.WriteLine(tree.DrawTree(""));
        }
    }

    class WuRoot : WorkUnitTestBase
    {
        public WuRoot(TestWorkUnitModel workUnit = null) : base(workUnit) {}
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
    class WuA : WorkUnitTestBase
    {
        public WuA(TestWorkUnitModel workUnit = null) : base(workUnit) { }
    }
    class WuB : WorkUnitTestBase
    {
        public WuB(TestWorkUnitModel workUnit = null) : base(workUnit) { }
    }
}

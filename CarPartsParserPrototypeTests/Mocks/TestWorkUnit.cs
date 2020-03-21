using CarPartsParser.Abstraction.Models;
using CarPartsParser.Models;
using CarPartsParser.Parser.Tree;
using CarPartsParser.SiteParsers.Abstraction.WorkUnits;
using System;

namespace CarPartsParserPrototypeTests.Mocks
{
    /// <summary>
    /// Base class for WorkUnit mocks. 
    /// Execute method returns WorkUnit provided during object construction. (It is done for simple setup of the mock behaviour)
    /// </summary>
    public class WorkUnitTestBase : IWorkUnit
    {
        internal TestWorkUnitModel workUnit;

        public WorkUnitTestBase(TestWorkUnitModel workUnit = null)
        {
            this.workUnit = workUnit ?? new TestWorkUnitModel();
        }

        public IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            return workUnit;
        }
    }

    /// <summary>
    /// WorkUnitModel for tests setup. It has <see cref="Path"/> property for execution path selection
    /// </summary>
    public class TestWorkUnitModel : IWorkUnitModel
    {
        public TestWorkUnitModel(ExecutionPath? path = null)
        {
            Path = path ?? ExecutionPath.Path1;
        }

        /// <summary>
        /// This value used by <see cref="TestWorkUnit.PathSelector"/> to select execution path
        /// </summary>
        public ExecutionPath Path { get; } = ExecutionPath.Path1;
    }

    public class TestWorkUnit
    {
        /// <summary>
        /// PathSelector function select execution path based on Path property of the <see cref="TestWorkUnitModel"/>
        /// </summary>
        public static Func<IWorkUnitModel, IWorkUnitModel, ExecutionPath> PathSelector = (i, o) => ((TestWorkUnitModel)i).Path;
    }
}

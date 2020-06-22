using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.Abstraction;
using ParserCoreProject.ParserCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCoreProjectTests.UnitTests.WorkerBase
{
    [TestClass]
    public class WBTestPreproc
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var workersContainer = new WorkersContainer<A, B>();

            var ep = new ExecutionPath();
            var prep = new ExecutionPathPreprocessor(ep);
            var ab = new ABFalseOverride(workersContainer, new IWorkerPreprocessor[] { prep });

            workersContainer.Add(ab);

            try
            {
                workersContainer.GetFirst().ParseAndExecuteNext(new A());
            }
            catch (Exception)
            {
            }

            //prep.ExecutionPath
        }
    }
}

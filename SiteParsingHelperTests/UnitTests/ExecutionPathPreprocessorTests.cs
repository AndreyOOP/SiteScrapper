using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.ParserCore;
using ParserCoreProjectTests.UnitTests.WorkerBase;
using System;

namespace ParserCoreProjectTests.UnitTests
{
    [TestClass]
    public class ExecutionPathPreprocessorTests
    {
        [TestMethod]
        public void Execute_WorkerExecuted_AddedToPath()
        {
            var preprocessor = new ExecutionPathPreprocessor(new ExecutionPath());

            preprocessor.Execute(new ABFalse(null));
            preprocessor.Execute(new BCFalse(null)); 

            Assert.AreEqual("ABFalse > BCFalse", preprocessor.ExecutionPath);
        }

        [TestMethod]
        public void Execute_WorkerAlreadyExecuted_ArgumentException()
        {
            var preprocessor = new ExecutionPathPreprocessor(new ExecutionPath());

            preprocessor.Execute(new ABFalse(null));

            var exception = Assert.ThrowsException<ArgumentException>(() => preprocessor.Execute(new ABFalse(null)));
            StringAssert.Contains(exception.Message, "Cycle reference");
        }

        [TestMethod]
        public void ParseAndExecuteNext_WorkerWithPreprocessor_ExpectedPath()
        {
            var executionPath = new ExecutionPath();

            var executionPathPreprocessor = new ExecutionPathPreprocessor(executionPath);

            var preprocessorContainer = new WorkerPreprocessorsContainer();
            preprocessorContainer.RegisterPreprocessor(executionPathPreprocessor);

            var sharedServices = new WorkerSharedServices<Result>(null, preprocessorContainer, new Result());

            var worker = new ABTrue(sharedServices);
            worker.ParseAndExecuteNext(new A());

            Assert.AreEqual("ABTrue", executionPath.ToString());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.Abstraction;
using ParserCoreProject.Exceptions;
using ParserCoreProject.ParserCore;
using System;

namespace ParserCoreProjectTests.UnitTests.WorkerBase
{
    [TestClass]
    public class WorkerBaseExceptionTests
    {
        IWorkersContainer<A, B> workersContainer;
        IWorkerSharedServices<A, B, Result> workerSharedServices;

        [TestInitialize]
        public void Initialize()
        {
            workerSharedServices = new WorkerSharedServices<A, B, Result>(new WorkersContainer<A, B>(), new WorkerPreprocessorsContainer(), null);
            workersContainer = workerSharedServices.WorkersContainer;
        }

        [TestMethod]
        public void ParseAndExecuteNext_SecondWorkerThrowsException_WorkUnitException()
        {
            var preprocessors = workerSharedServices.WorkersPreprocessorsContainer;
            preprocessors.RegisterPreprocessor(new ExecutionPathPreprocessor(new ExecutionPath()));

            var workerAB = new ABFalse(workerSharedServices);
            var exceptionBC = new BCException(workerSharedServices);

            workersContainer.Add(workerAB);
            workersContainer.Add(exceptionBC);

            var workUnitException = Assert.ThrowsException<WorkUnitException>(() => workerAB.ParseAndExecuteNext(new A()));
            Assert.AreEqual(typeof(Exception), workUnitException.InnerException.GetType());
            Assert.AreEqual("ABFalse > BCException", workUnitException.ExecutionPath);
        }

        [TestMethod]
        public void ParseAndExecuteNext_SecondWorkerThrowsExceptionThreeWorkersInChain_WorkUnitException()
        {
            var preprocessors = workerSharedServices.WorkersPreprocessorsContainer;
            preprocessors.RegisterPreprocessor(new ExecutionPathPreprocessor(new ExecutionPath()));

            var workerAB = new ABFalse(workerSharedServices);
            var exceptionBC = new BCException(workerSharedServices);
            var workerCDTrue = new CDTrue(workerSharedServices);

            workersContainer.Add(workerAB);
            workersContainer.Add(exceptionBC);
            workersContainer.Add(workerCDTrue);

            var workUnitException = Assert.ThrowsException<WorkUnitException>(() => workerAB.ParseAndExecuteNext(new A()));
            Assert.AreEqual(typeof(Exception), workUnitException.InnerException.GetType());
            Assert.AreEqual("ABFalse > BCException", workUnitException.ExecutionPath);
        }
    }
}

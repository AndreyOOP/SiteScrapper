using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.Abstraction;
using ParserCoreProject.ParserCore;

namespace ParserCoreProjectTests.UnitTests.WorkerBase
{
    [TestClass]
    public class WorkerBasePreprocessorTests
    {
        IWorkerPreprocessorsContainer preprocessorContainer;
        IWorkersContainer workersContainer;
        ABTrue worker;

        [TestInitialize]
        public void Initialize()
        {
            var workerSharedServices = new WorkerSharedServices<Result>(new WorkersContainer<A, B>(), new WorkerPreprocessorsContainer(), new Result());

            preprocessorContainer = workerSharedServices.WorkersPreprocessorsContainer;

            workersContainer = workerSharedServices.WorkersContainer;
            worker = new ABTrue(workerSharedServices);
            workersContainer.Add(worker);
        }

        [TestMethod]
        public void ParseAndExecuteNext_NoPreprocessorRegistered_WorkerExecuted()
        {
            worker.ParseAndExecuteNext(new A());
        }

        [TestMethod]
        public void ParseAndExecuteNext_RegisterPreprocessor_ExpectedPreprocessorExecuted()
        {
            var preprocessor = new TestPreprocessorA();
            preprocessorContainer.RegisterPreprocessor(preprocessor);

            worker.ParseAndExecuteNext(new A());

            Assert.AreEqual("Worker is ABTrue; TestPreprocessorA Executed", preprocessor.Status);
        }

        [TestMethod]
        public void ParseAndExecuteNext_RegisterFewPreprocessors_ExpectedPreprocessorsExecuted()
        {
            var preprocessorA = new TestPreprocessorA();
            var preprocessorB = new TestPreprocessorB();

            preprocessorContainer.RegisterPreprocessor(preprocessorA);
            preprocessorContainer.RegisterPreprocessor(preprocessorB);

            worker.ParseAndExecuteNext(new A());

            Assert.AreEqual("Worker is ABTrue; TestPreprocessorA Executed", preprocessorA.Status);
            Assert.AreEqual("Worker is ABTrue; TestPreprocessorB Executed", preprocessorB.Status);
        }
    }

    class TestPreprocessorA : IWorkerPreprocessor
    {
        public string Status { get; set; }

        public void Execute(object worker)
        {
            Status = $"Worker is {worker.GetType().Name}; {nameof(TestPreprocessorA)} Executed";
        }
    }

    class TestPreprocessorB : IWorkerPreprocessor
    {
        public string Status { get; set; }

        public void Execute(object worker)
        {
            Status = $"Worker is {worker.GetType().Name}; {nameof(TestPreprocessorB)} Executed";
        }
    }
}

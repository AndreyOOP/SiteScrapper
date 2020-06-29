using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.ParserCore;
using ParserCoreProjectTests.ParserCore;
using System;

namespace ParserCoreTests.UnitTests.WorkersContainer
{
    [TestClass]
    public class WorkersContainerTests
    {
        WorkersContainerProtectedAccessor workersContainer;

        [TestInitialize]
        public void Initialize()
        {
            workersContainer = new WorkersContainerProtectedAccessor();
        }

        // Test knows about class implementation
        // From other hand how to check if it is ok? As well shows expected behaviour
        [TestMethod]
        public void Add_DifferentWorkers_Ok()
        {
            var workerAB = new WorkerAB();
            var workerAC = new WorkerAC();

            workersContainer.Add(workerAB);
            workersContainer.Add(workerAC);

            Assert.AreEqual(2, workersContainer.Workers.Count);
            Assert.AreEqual(workerAB, workersContainer.Workers[new InOutKey<A, B>()] );
            Assert.AreEqual(workerAC, workersContainer.Workers[new InOutKey<A, C>()] );
        }

        [TestMethod]
        public void Add_SameWorkers_ArgumentException()
        {
            workersContainer.Add(new WorkerAB());

            var exception = Assert.ThrowsException<ArgumentException>(() => workersContainer.Add(new WorkerAB()));
            Assert.AreEqual("Worker<A, B> already set", exception.Message);
        }

        // Maybe it is better to prohibit such worker types
        [TestMethod]
        public void Add_ReverseWorkerAndSameTypeWorker_Ok()
        {
            var workerAB = new WorkerAB();
            var workerBA = new WorkerBA(); // allowed to add reverse worker BA - reversed of AB
            var workerAA = new WorkerAA(); // allowed to add worker which are not convert type A to A

            workersContainer.Add(workerAB);
            workersContainer.Add(workerBA);
            workersContainer.Add(workerAA);

            Assert.AreEqual(3, workersContainer.Workers.Count);
        }

        [TestMethod]
        public void Get_WorkersRegistered_GetSucceed()
        {
            var workerAB = new WorkerAB();
            var workerAC = new WorkerAC();
            var workerBA = new WorkerBA();
            var workerAA = new WorkerAA();

            workersContainer.Add(workerAA);
            workersContainer.Add(workerAB);
            workersContainer.Add(workerBA);
            workersContainer.Add(workerAC);

            Assert.AreEqual(workerAC, workersContainer.Get<A, C>());
            Assert.AreEqual(workerBA, workersContainer.Get<B, A>());
            Assert.AreEqual(workerAB, workersContainer.Get<A, B>());
            Assert.AreEqual(workerAA, workersContainer.Get<A, A>());
        }

        [TestMethod]
        public void Get_WorkerIsNotRegistered_ArgumentException()
        {
            workersContainer.Add(new WorkerAB());

            Assert.ThrowsException<ArgumentException>(() => workersContainer.Get<A, C>());
        }

        [TestMethod]
        public void Get_UnexpectedWorkerType_InvalidCastException()
        {
            workersContainer.Workers[new InOutKey<A, B>()] = typeof(string); // some unexpected type != typeof(IWorker<TIn, TOut>)

            Assert.ThrowsException<InvalidCastException>(() => workersContainer.Get<A, B>());
        }

        [TestMethod]
        public void GetIfSingleImplementation_FewWorkerImplementationsForTypeA_ArgumentException()
        {
            workersContainer.Add(new WorkerAB());
            workersContainer.Add(new WorkerAC());

            var exception = Assert.ThrowsException<ArgumentException>(() => workersContainer.GetIfSingleImplementation<A>());

            StringAssert.Contains(exception.Message, "has few implementations");
        }

        [TestMethod]
        public void GetIfSingleImplementation_NoWorkerImplementationsForTypeA_ArgumentException()
        {
            workersContainer.Add(new WorkerAB());
            workersContainer.Add(new WorkerAC());

            var exception = Assert.ThrowsException<ArgumentException>(() => workersContainer.GetIfSingleImplementation<B>());

            StringAssert.Contains(exception.Message, "is not registered");
        }

        [TestMethod]
        public void GetIfSingleImplementation_SingleWorkerWithAInputType_GetAndExecuted()
        {
            workersContainer.Add(new WorkerAB());
            workersContainer.Add(new WorkerBA());

            dynamic workerAB = workersContainer.GetIfSingleImplementation<A>();

            workerAB.ParseAndExecuteNext(new A()); // call to ParseAndExecuteNext success - note disadvantage that dynamic object

            Assert.AreEqual(typeof(WorkerAB), workerAB.GetType());
        }
    }
}

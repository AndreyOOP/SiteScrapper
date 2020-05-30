using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProjectTests.ParserCore;
using System;

namespace ParserCoreTests.ParserCore.TestWorkersContainer
{
    [TestClass]
    public class WorkersContainerTests
    {
        // Test knows about class implementation
        // From other hand how to check if it is ok? As well shows expected behaviour
        [TestMethod]
        public void Add_DifferentWorkers_Ok()
        {
            var workerAB = new WorkerAB();
            var workerAC = new WorkerAC();
            var workersContainer = new WorkersContainerProtectedAccessor();

            workersContainer.Add(workerAB);
            workersContainer.Add(workerAC);

            Assert.AreEqual(2, workersContainer.Workers.Count);
            Assert.AreEqual(workerAB, workersContainer.Workers[new Tuple<Type, Type>(typeof(A), typeof(B))] );
            Assert.AreEqual(workerAC, workersContainer.Workers[new Tuple<Type, Type>(typeof(A), typeof(C))] );
        }

        [TestMethod]
        public void Add_SameWorkers_ArgumentException()
        {
            var workersContainer = new WorkersContainerProtectedAccessor();

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
            var workersContainer = new WorkersContainerProtectedAccessor();

            workersContainer.Add(workerAB);
            workersContainer.Add(workerBA);
            workersContainer.Add(workerAA);

            Assert.AreEqual(3, workersContainer.Workers.Count);
        }
    }
}

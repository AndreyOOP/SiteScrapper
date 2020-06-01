using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.ParserCore;
using System;

namespace ParserCoreProjectTests.UnitTests.WorkerBase
{
    [TestClass]
    public class WorkerBaseTests
    {
        private WorkersContainer<A, B> workersContainer;

        [TestInitialize]
        public void Initialize()
        {
            workersContainer = new WorkersContainer<A, B>();
        }

        [TestMethod]
        public void ParseAndExecuteNext_StopHereTrueNoNextWorker_Ok()
        {
            var workerAB = new ABTrue(workersContainer);
            workersContainer.Add(workerAB);

            workerAB.ParseAndExecuteNext(new A());
            Assert.AreEqual("ABTrue ParseUnit executed. StopHere = True", workerAB.Status);
        }

        [TestMethod]
        public void ParseAndExecuteNext_StopHereTrueWithNextWorker_Ok()
        {
            var workerAB = new ABTrue(workersContainer);
            var workerBC = new BCFalse(workersContainer);
            workersContainer.Add(workerAB);
            workersContainer.Add(workerBC);

            workerAB.ParseAndExecuteNext(new A());
            
            Assert.AreEqual("ABTrue ParseUnit executed. StopHere = True", workerAB.Status);
            Assert.AreEqual(null, workerBC.Status);
        }

        [TestMethod]
        public void ParseAndExecuteNext_StopHereFalseNoNextWorker_ArgumentException()
        {
            var workerAB = new ABFalse(workersContainer);
            workersContainer.Add(workerAB);

            var exception = Assert.ThrowsException<ArgumentException>(() => workerAB.ParseAndExecuteNext(new A()));
            Assert.AreEqual("ABFalse ParseUnit executed. StopHere = False", workerAB.Status);
            Assert.AreEqual("Worker<A, AnyOtherType> is not registered", exception.Message);
        }

        [TestMethod]
        public void ParseAndExecuteNext_ThreeWorkersInChain_Ok()
        {
            var workerAB = new ABFalse(workersContainer);
            var workerBC = new BCFalse(workersContainer);
            var workerCD = new CDTrue(workersContainer);

            workersContainer.Add(workerAB);
            workersContainer.Add(workerBC);
            workersContainer.Add(workerCD);

            workersContainer.GetFirst().ParseAndExecuteNext(new A());

            Assert.AreEqual("ABFalse ParseUnit executed. StopHere = False", workerAB.Status);
            Assert.AreEqual("BCFalse ParseUnit executed. StopHere = False", workerBC.Status);
            Assert.AreEqual("CDTrue ParseUnit executed. StopHere = True", workerCD.Status);
        }

        [TestMethod]
        public void ParseAndExecuteNext_FewWorkersCouldBExecutedNext_ArgumentException()
        {
            var workerAB = new ABFalse(workersContainer);
            var workerBC = new BCFalse(workersContainer);
            var workerBE = new BEFalse(workersContainer);

            workersContainer.Add(workerAB);
            workersContainer.Add(workerBC);
            workersContainer.Add(workerBE);

            var exception = Assert.ThrowsException<ArgumentException>(() => workerAB.ParseAndExecuteNext(new A()));
            StringAssert.StartsWith(exception.Message, "Worker has few implementations");
        }

        [TestMethod]
        public void ParseAndExecuteNext_FewWorkersCouldBExecutedNextSelectionOverrided_Ok()
        {
            var workerAB = new ABFalseOverride(workersContainer);
            var workerBC = new BCFalse(workersContainer);
            var workerBE = new BEFalse(workersContainer);
            var workerCD = new CDTrue(workersContainer);
            var workerED = new EDTrue(workersContainer);

            workersContainer.Add(workerAB);
            workersContainer.Add(workerBC);
            workersContainer.Add(workerBE);
            workersContainer.Add(workerCD);
            workersContainer.Add(workerED);

            workersContainer.GetFirst().ParseAndExecuteNext(new A());

            Assert.AreEqual("ABFalseOverride ParseUnit executed. StopHere = False", workerAB.Status);
            Assert.AreEqual("BCFalse ParseUnit executed. StopHere = False", workerBC.Status);
            Assert.AreEqual("BEFalse ParseUnit executed. StopHere = False", workerBE.Status);
            Assert.AreEqual("CDTrue ParseUnit executed. StopHere = True", workerCD.Status);
            Assert.AreEqual("EDTrue ParseUnit executed. StopHere = True", workerED.Status);
        }
    }
}

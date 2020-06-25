using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.Abstraction;
using ParserCoreProject.Exceptions;
using ParserCoreProject.ParserCore;
using System;

namespace ParserCoreProjectTests.UnitTests.WorkerBase
{
    [TestClass]
    public class WorkerBaseTests
    {
        Result result;
        IWorkersContainer<A, B> workersContainer;
        IWorkerSharedServices<A, B, Result> workerSharedServices;

        [TestInitialize]
        public void Initialize()
        {
            result = new Result();
            workerSharedServices = new WorkerSharedServices<A, B, Result>(new WorkersContainer<A, B>(), new WorkerPreprocessorsContainer(), result);
            workersContainer = workerSharedServices.WorkersContainer;
        }

        [TestMethod]
        public void ParseAndExecuteNext_StopHereTrueNoNextWorker_Ok()
        {
            var workerAB = new ABTrue(workerSharedServices);
            workersContainer.Add(workerAB);

            workerAB.ParseAndExecuteNext(new A());
            Assert.AreEqual("ABTrue ParseUnit executed. StopHere = True", workerAB.Status);
        }

        [TestMethod]
        public void ParseAndExecuteNext_StopHereTrueWithNextWorker_Ok()
        {
            var workerAB = new ABTrue(workerSharedServices);
            var workerBC = new BCFalse(workerSharedServices);
            workersContainer.Add(workerAB);
            workersContainer.Add(workerBC);

            workerAB.ParseAndExecuteNext(new A());
            
            Assert.AreEqual("ABTrue ParseUnit executed. StopHere = True", workerAB.Status);
            Assert.AreEqual(null, workerBC.Status);
        }

        [TestMethod]
        public void ParseAndExecuteNext_StopHereFalseNoNextWorker_ArgumentException()
        {
            var workerAB = new ABFalse(workerSharedServices);
            workersContainer.Add(workerAB);

            var exception = Assert.ThrowsException<WorkUnitException>(() => workerAB.ParseAndExecuteNext(new A()));
            Assert.AreEqual(typeof(ArgumentException), exception.InnerException.GetType());
            Assert.AreEqual("ABFalse ParseUnit executed. StopHere = False", workerAB.Status);
            Assert.AreEqual("Worker<A, AnyOtherType> is not registered", exception.InnerException.Message);
        }

        [TestMethod]
        public void ParseAndExecuteNext_ThreeWorkersInChain_Ok()
        {
            var workerAB = new ABFalse(workerSharedServices);
            var workerBC = new BCFalse(workerSharedServices);
            var workerCD = new CDTrue(workerSharedServices);

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
            var workerAB = new ABFalse(workerSharedServices);
            var workerBC = new BCFalse(workerSharedServices);
            var workerBE = new BEFalse(workerSharedServices);

            workersContainer.Add(workerAB);
            workersContainer.Add(workerBC);
            workersContainer.Add(workerBE);

            var exception = Assert.ThrowsException<WorkUnitException>(() => workerAB.ParseAndExecuteNext(new A()));
            Assert.AreEqual(typeof(ArgumentException), exception.InnerException.GetType());
            StringAssert.StartsWith(exception.InnerException.Message, "Worker has few implementations");
        }

        [TestMethod]
        public void ParseAndExecuteNext_FewWorkersCouldBExecutedNextSelectionOverrided_Ok()
        {
            var workerAB = new ABFalseOverride(workerSharedServices);
            var workerBC = new BCFalse(workerSharedServices);
            var workerBE = new BEFalse(workerSharedServices);
            var workerCD = new CDTrue(workerSharedServices);
            var workerED = new EDTrue(workerSharedServices);

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

        [TestMethod]
        public void ParseAndExecuteNext_SingleWorker_ResultUpdated()
        {
            var workerAB = new ABTrue(workerSharedServices);
            workersContainer.Add(workerAB);

            workerAB.ParseAndExecuteNext(new A());

            Assert.AreEqual("Set value in ABFalse", workerSharedServices.Result.TestStatus);
        }
    }
}

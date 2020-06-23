using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.Abstraction;
using ParserCoreProject.ParserCore;
using System;
using System.Linq;

namespace ParserCoreProjectTests.UnitTests
{
    [TestClass]
    public class WorkerPreprocessorsContainerTests
    {
        private WorkerPreprocessorsContainer preprocessorsContainer;

        [TestInitialize]
        public void Initialize()
        {
            preprocessorsContainer = new WorkerPreprocessorsContainer();
        }

        [TestMethod]
        public void RegisterPreprocessor_Null_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => preprocessorsContainer.RegisterPreprocessor(null));
        }

        [TestMethod]
        public void GetPreprocessors_RegisterTwo_GetTwo()
        {
            preprocessorsContainer.RegisterPreprocessor(new TestPreprocessor());
            preprocessorsContainer.RegisterPreprocessor(new TestPreprocessor());

            Assert.AreEqual(2, preprocessorsContainer.GetPreprocessors().Count());
        }
    }

    class TestPreprocessor : IWorkerPreprocessor
    {
        public void Execute(object worker)
        {
        }
    }
}

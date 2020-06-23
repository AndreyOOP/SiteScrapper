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

        [TestMethod]
        public void GetPreprocessors_ZeroRegistered_BlankCollection()
        {
            Assert.AreEqual(0, preprocessorsContainer.GetPreprocessors().Count());
        }

        [TestMethod]
        public void GetPreprocessor_PreprocessorTRegistered_GetT()
        {
            preprocessorsContainer.RegisterPreprocessor(new TestPreprocessor());
            preprocessorsContainer.RegisterPreprocessor(new TestPreprocessorB());

            Assert.AreEqual(typeof(TestPreprocessorB), preprocessorsContainer.GetPreprocessor<TestPreprocessorB>().GetType());
        }

        [TestMethod]
        public void GetPreprocessor_PreprocessorTNotRegistered_Null()
        {
            preprocessorsContainer.RegisterPreprocessor(new TestPreprocessor());

            Assert.IsNull(preprocessorsContainer.GetPreprocessor<TestPreprocessorB>());
        }

        [TestMethod]
        public void GetPreprocessor_BlankCollection_Null()
        {
            Assert.IsNull(preprocessorsContainer.GetPreprocessor<TestPreprocessorB>());
        }
    }

    class TestPreprocessor : IWorkerPreprocessor
    {
        public void Execute(object worker)
        {
        }
    }
    class TestPreprocessorB : IWorkerPreprocessor
    {
        public void Execute(object worker)
        {
        }
    }
}

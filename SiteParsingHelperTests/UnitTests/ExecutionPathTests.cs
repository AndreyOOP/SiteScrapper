using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCoreProject.ParserCore;
using System;

namespace ParserCoreProjectTests.UnitTests
{
    [TestClass]
    public class ExecutionPathTests
    {
        private ExecutionPath executionPath;

        [TestInitialize]
        public void Initialize()
        {
            executionPath = new ExecutionPath();
        }

        [TestMethod]
        public void AlreadyExecuted_Executed_True()
        {
            executionPath.Add("unit1");

            Assert.IsTrue(executionPath.AlreadyExecuted("unit1"));
        }

        [TestMethod]
        public void AlreadyExecuted_NotExecuted_False()
        {
            executionPath.Add("unit1");

            Assert.IsFalse(executionPath.AlreadyExecuted("not_executed_unit"));
        }

        [TestMethod]
        public void AlreadyExecuted_NoUnitsExecuted_False()
        {
            Assert.IsFalse(executionPath.AlreadyExecuted("not_executed_unit"));
        }

        [TestMethod]
        public void ToString_NoUnitExecuted_Blank()
        {
            Assert.AreEqual("", executionPath.ToString());
        }

        [TestMethod]
        public void ToString_UnitsExecuted_Path()
        {
            executionPath.Add("unit1");
            executionPath.Add("unit2");
            executionPath.Add("unit3");

            Assert.AreEqual("unit1 > unit2 > unit3", executionPath.ToString());
        }

        [TestMethod]
        public void Add_AddSamePathTwice_ArgumentException()
        {
            executionPath.Add("unit1");
            Assert.ThrowsException<ArgumentException>(() => executionPath.Add("unit1"));
        }
    }
}

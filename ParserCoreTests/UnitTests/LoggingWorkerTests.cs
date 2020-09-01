using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore;
using ParserCore.Abstraction;
using System;
using System.Linq;

namespace UnitTests.LoggingWorkerTests
{
    #region Test Types
    class In 
    {
        public string Type { get; set; }
    }

    class Out { }

    class ShowStackTraceOnly : WorkerLogSettings
    {
        public override bool ShowStackTrace => true;
        public override bool ShowExceptionMessage => false;
        public override bool AddInModel => false;
        public override bool LogOnlyParseMethod => false;
    }

    class ShowExceptionMessageOnly : WorkerLogSettings
    {
        public override bool ShowStackTrace => false;
        public override bool ShowExceptionMessage => true;
        public override bool AddInModel => false;
        public override bool LogOnlyParseMethod => false;
    }

    class LogAllForParseMethod : WorkerLogSettings
    {
        public override bool ShowStackTrace => true;
        public override bool ShowExceptionMessage => true;
        public override bool AddInModel => true;
        public override bool LogOnlyParseMethod => true;
    }

    class WorkerMock : IWorker<In, Out>
    {
        public bool IsExecutable(In model)
        {
            if (model.Type == "exception")
                throw new Exception("Exception during IsExecutable execution");

            return true;
        }

        public Out Parse(In model)
        {
            if (model.Type == "exception")
                throw new Exception("Exception during Parse execution");

            return new Out();
        }
    }
    #endregion

    [TestClass]
    public class LoggingWorkerTests_ShowStackTraceOnly
    {
        InMemoryWorkerLogger logger;
        LoggingWorker<In, Out> loggingWorker;

        [TestInitialize]
        public void Initialize()
        {
            logger = new InMemoryWorkerLogger();
            loggingWorker = new LoggingWorker<In, Out>(new WorkerMock(), logger, new ShowStackTraceOnly());
        }

        [TestMethod]
        public void IsExecutable_NoException_ShowStackTrace()
        {
            loggingWorker.IsExecutable(new In { Type = "ok" });

            var logRecord = logger.Records.First();

            Assert.IsNull(logRecord.StackTrace);
            Assert.IsNull(logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
            Assert.AreEqual(nameof(WorkerMock), logRecord.WorkerName);
            Assert.AreEqual("IsExecutable", logRecord.MethodName);
            Assert.IsTrue(logRecord.Created <= DateTime.UtcNow);
        }

        [TestMethod]
        public void IsExecutable_Exception_ShowStackTrace()
        {
            Assert.ThrowsException<Exception>(() => loggingWorker.IsExecutable(new In { Type = "exception" }));

            var logRecord = logger.Records.First();

            Assert.IsNotNull(logRecord.StackTrace);
            Assert.IsNull(logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
            Assert.AreEqual(nameof(WorkerMock), logRecord.WorkerName);
            Assert.AreEqual("IsExecutable", logRecord.MethodName);
            Assert.IsTrue(logRecord.Created <= DateTime.UtcNow);
        }

        [TestMethod]
        public void Parse_NoException_ShowStackTrace()
        {
            loggingWorker.Parse(new In { Type = "ok" });

            var logRecord = logger.Records.First();

            Assert.IsNull(logRecord.StackTrace);
            Assert.IsNull(logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
            Assert.AreEqual(nameof(WorkerMock), logRecord.WorkerName);
            Assert.AreEqual("Parse", logRecord.MethodName);
            Assert.IsTrue(logRecord.Created <= DateTime.UtcNow);
        }

        [TestMethod]
        public void Parse_Exception_ShowStackTrace()
        {
            Assert.ThrowsException<Exception>(() => loggingWorker.Parse(new In { Type = "exception" }));

            var logRecord = logger.Records.First();

            Assert.IsNotNull(logRecord.StackTrace);
            Assert.IsNull(logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
            Assert.AreEqual(nameof(WorkerMock), logRecord.WorkerName);
            Assert.AreEqual("Parse", logRecord.MethodName);
            Assert.IsTrue(logRecord.Created <= DateTime.UtcNow);
        }
    }

    [TestClass]
    public class LoggingWorkerTests_ShowExceptionMessageOnly
    {
        InMemoryWorkerLogger logger;
        LoggingWorker<In, Out> loggingWorker;

        [TestInitialize]
        public void Initialize()
        {
            logger = new InMemoryWorkerLogger();
            loggingWorker = new LoggingWorker<In, Out>(new WorkerMock(), logger, new ShowExceptionMessageOnly());
        }

        [TestMethod]
        public void IsExecutable_NoException_ShowStackTrace()
        {
            loggingWorker.IsExecutable(new In { Type = "ok" });

            var logRecord = logger.Records.First();

            Assert.IsNull(logRecord.StackTrace);
            Assert.IsNull(logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
        }

        [TestMethod]
        public void IsExecutable_Exception_ShowStackTrace()
        {
            var ex = Assert.ThrowsException<Exception>(() => loggingWorker.IsExecutable(new In { Type = "exception" }));

            var logRecord = logger.Records.First();

            Assert.IsNull(logRecord.StackTrace);
            Assert.AreEqual(ex.Message, logRecord.ExceptionMessage);
            Assert.AreEqual("Exception during IsExecutable execution", logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
        }

        [TestMethod]
        public void Parse_NoException_ShowStackTrace()
        {
            loggingWorker.Parse(new In { Type = "ok" });

            var logRecord = logger.Records.First();

            Assert.IsNull(logRecord.StackTrace);
            Assert.IsNull(logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
        }

        [TestMethod]
        public void Parse_Exception_ShowStackTrace()
        {
            var ex = Assert.ThrowsException<Exception>(() => loggingWorker.Parse(new In { Type = "exception" }));

            var logRecord = logger.Records.First();

            Assert.IsNull(logRecord.StackTrace);
            Assert.AreEqual(ex.Message, logRecord.ExceptionMessage);
            Assert.AreEqual("Exception during Parse execution", logRecord.ExceptionMessage);
            Assert.IsNull(logRecord.InModel);
        }
    }

    [TestClass]
    public class LoggingWorkerTests_LogAllForParseMaethod
    {
        InMemoryWorkerLogger logger;
        LoggingWorker<In, Out> loggingWorker;

        [TestInitialize]
        public void Initialize()
        {
            logger = new InMemoryWorkerLogger();
            loggingWorker = new LoggingWorker<In, Out>(new WorkerMock(), logger, new LogAllForParseMethod());
        }

        [TestMethod]
        public void IsExecutable_NoException_LogRecordNotAdded()
        {
            loggingWorker.IsExecutable(new In { Type = "ok" });

            Assert.AreEqual(0, logger.Records.Count);
        }

        [TestMethod]
        public void IsExecutable_Exception_LogRecordNotAdded()
        {
            var ex = Assert.ThrowsException<Exception>(() => loggingWorker.IsExecutable(new In { Type = "exception" }));

            Assert.AreEqual(0, logger.Records.Count);
        }

        [TestMethod]
        public void Parse_NoException_LogRecordAsExpected()
        {
            loggingWorker.Parse(new In { Type = "ok" });

            var logRecord = logger.Records.First();

            Assert.IsNull(logRecord.StackTrace);
            Assert.IsNull(logRecord.ExceptionMessage);
            Assert.IsNotNull(logRecord.InModel);
            Assert.AreEqual(nameof(WorkerMock), logRecord.WorkerName);
            Assert.AreEqual("Parse", logRecord.MethodName);
            Assert.IsTrue(logRecord.Created <= DateTime.UtcNow);
        }

        [TestMethod]
        public void Parse_Exception_LogRecordAsExpected()
        {
            var ex = Assert.ThrowsException<Exception>(() => loggingWorker.Parse(new In { Type = "exception" }));

            var logRecord = logger.Records.First();

            Assert.IsNotNull(logRecord.StackTrace);
            Assert.AreEqual(ex.Message, logRecord.ExceptionMessage);
            Assert.AreEqual("Exception during Parse execution", logRecord.ExceptionMessage);
            Assert.IsNotNull(logRecord.InModel);
            Assert.AreEqual(nameof(WorkerMock), logRecord.WorkerName);
            Assert.AreEqual("Parse", logRecord.MethodName);
            Assert.IsTrue(logRecord.Created <= DateTime.UtcNow);
        }
    }
}

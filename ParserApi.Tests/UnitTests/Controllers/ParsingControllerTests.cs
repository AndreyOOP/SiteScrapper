using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParserApi.Controllers;
using ParserApi.Controllers.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ParsingControllerTests
    {
        Site911Parser parser;
        ParsingController controller;

        [TestInitialize]
        public void Initialize()
        {
            var parserMock = new Mock<Site911Parser>(MockBehavior.Strict, new object[] { null }); // new object[] - constructor parameters
            parserMock.Setup(p => p.Parse(It.IsAny<In>()))
                .Returns(new Result());
            parser = parserMock.Object;

            var memoryLogger = new InMemoryWorkerLogger();
            memoryLogger.Records.Add(new WorkerLogRecord());
            memoryLogger.Records.Add(new WorkerLogRecord { ExceptionMessage = "Msg" });
            memoryLogger.Records.Add(new WorkerLogRecord { StackTrace = "Trace" });
            memoryLogger.Records.Add(new WorkerLogRecord
            {
                ExceptionMessage = "Msg",
                StackTrace = "Trace"
            });
            
            controller = new ParsingController(memoryLogger, parser);
        }

        [TestMethod]
        public void ParseSingleModel_ShowLogWithErrors_AllLogRecords()
        {
            var result = (Result)controller.ParseSingleModel("id", new RequestParams
            {
                ShowLog = true
            });

            Assert.AreEqual(4, result.Log.Count());
        }

        [TestMethod]
        public void ParseSingleModel_NotShowLogError_ErrorRecords()
        {
            var memoryLogger = new InMemoryWorkerLogger();
            memoryLogger.Records.Add(new WorkerLogRecord { ExceptionMessage = "Msg" });

            controller = new ParsingController(memoryLogger, parser);

            var result = (Result)controller.ParseSingleModel("id", new RequestParams());

            Assert.AreEqual(1, result.Log.Count());
        }

        [TestMethod]
        public void ParseSingleModel_NotShowLogErrors_ErrorRecordsOnly()
        {
            var result = (Result)controller.ParseSingleModel("id", new RequestParams
            {
                ShowLog = false
            });

            Assert.AreEqual(3, result.Log.Count());
        }

        [TestMethod]
        public void ParseSingleModel_NotShowDefaultLogErrors_ErrorRecordsOnly()
        {
            var result = (Result)controller.ParseSingleModel("id", new RequestParams());

            Assert.AreEqual(3, result.Log.Count());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParserApi.Controllers;
using ParserApi.Controllers.Models;
using ParserApi.Parsers.Autoklad;
using ParserApi.Parsers.Autoklad.Models;
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
            var memoryLogger = new InMemoryWorkerLogger();
            memoryLogger.Records.Add(new WorkerLogRecord());
            memoryLogger.Records.Add(new WorkerLogRecord { ExceptionMessage = "Msg" });
            memoryLogger.Records.Add(new WorkerLogRecord { StackTrace = "Trace" });
            memoryLogger.Records.Add(new WorkerLogRecord
            {
                ExceptionMessage = "Msg",
                StackTrace = "Trace"
            });

            var parserMock = new Mock<Site911Parser>(MockBehavior.Strict, new object[] { null, memoryLogger }); // new object[] - constructor parameters
            parserMock.Setup(p => p.Parse(It.IsAny<In911>()))
                .Returns(new Result911());
            parser = parserMock.Object;

            var autokladMock = new Mock<AutokladParser>(MockBehavior.Strict, new object[] { null, memoryLogger });
            autokladMock.Setup(p => p.Parse(It.IsAny<InAK>()))
                .Returns(new ResultAK());

            controller = new ParsingController(parser, autokladMock.Object);
        }

        [TestMethod]
        public void ParseSingleModel_ShowLogWithErrors_AllLogRecords()
        {
            var result = (ParsersResult)controller.ParseSingleModel("id", new RequestParams
            {
                ShowLog = true
            });

            Assert.AreEqual(4, result.Site911Log.Count());
        }

        [TestMethod]
        public void ParseSingleModel_NotShowLogErrors_ErrorRecordsOnly()
        {
            var result = (ParsersResult)controller.ParseSingleModel("id", new RequestParams
            {
                ShowLog = false
            });

            Assert.AreEqual(3, result.Site911Log.Count());
        }

        [TestMethod]
        public void ParseSingleModel_NotShowDefaultLogErrors_ErrorRecordsOnly()
        {
            var result = (ParsersResult)controller.ParseSingleModel("id", new RequestParams());

            Assert.AreEqual(3, result.Site911Log.Count());
        }
    }
}

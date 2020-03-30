using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.Models;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using ParserApi.Parsers.Site911Parser.WorkUnits.Models;
using ParserApi.Tests.Mocks;
using SiteParsingHelper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ParserApi.Tests.MockedDataAcceptanceTests.Site911.WorkUnits
{
    // Below the sample how to setup tests with mocked HttpClient (HttpMessageHandlerMock)
    // Could be usefull in end to end testing not sure if it is good idea to test get requests...
    [TestClass]
    public class A0httpRequestTests
    {
        private A0httpRequest workUnit;

        [TestInitialize]
        public void Initialize()
        {
            // Note: need to find something better, - such test knows about inner usage of id, maybe better setup is [InputModel] = HttpResponseMessage
            // alternatively just use Moq
            var handlerMock = new HttpMessageHandlerMock(new Dictionary<string, HttpResponseMessage>
            {
                ["http://911auto.com.ua/search/ok"] = new HttpResponseMessage { Content = new StringContent("html content"), StatusCode = HttpStatusCode.OK },
                ["http://911auto.com.ua/search/exception"] = new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError }
            });
            var httpClient = new HttpClient(handlerMock);
            workUnit = new A0httpRequest(httpClient);
        }

        [TestMethod]
        public void Execute_CorrrectId_ReturnHtmlContent()
        {
            var inModel = new Input { Id = "ok" };
            var result = (ParserExecutorResultBase)new Site911Result();
            
            var workUnitResult = (A0httpResponse)(workUnit.Execute(inModel, ref result));

            Assert.AreEqual("html content", workUnitResult.Html);
        }

        [TestMethod]
        public void Execute_Incorrrect_ResultWithException()
        {
            var inModel = new Input { Id = "exception" };
            var result = (ParserExecutorResultBase)new Site911Result();

            var workUnitResult = (A0httpResponse)(workUnit.Execute(inModel, ref result));

            Assert.AreEqual("Get Request Status is Not 200OK", result.Exception);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.Models;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using ParserApi.Parsers.Site911Parser.WorkUnits.Models;
using SiteParsingHelper;
using System.Net.Http;

namespace ParserApi.RealDataAcceptanceTests.Site911.WorkUnits
{
    [TestClass]
    public class A0httpRequestTests
    {
        [TestMethod]
        public void Execute_RealModelSample_GetContentNoException()
        {
            var httpClient = new HttpClient();

            var workUnit = new A0httpRequest(httpClient);

            var inModel = new Input { Id = "MD619865" };
            var result = (ParserExecutorResultBase)new Site911Result();
            var firstResult = (A0httpResponse)(workUnit.Execute(inModel, ref result));

            Assert.IsNull(result.Exception);
            Assert.IsTrue(firstResult.Html.Length > 0);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers._911Site.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.Models;
using SiteParsingHelper;
using System.IO;
using System.Reflection;

namespace ParserApi.Tests.MockedDataAcceptanceTests.Site911.WorkUnits
{
    [TestClass]
    public class B0parseHtmlTest
    {
        // move to unit tests - ?
        [TestMethod]
        public void Execute_B0parseHtml_AsExpected()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"MockedDataAcceptanceTests\Data\A0httpResponse.html");

            var a0httpResponse = new A0httpResponse
            {
                Html = File.ReadAllText(path)
            };
            var siteResult = new Site911Result();
            var result = (ParserExecutorResultBase)siteResult;

            var workUnit = new B0parseHtml();
            var workUnitResult = (B0parseHtmlResult)(workUnit.Execute(a0httpResponse, ref result));

            Assert.AreEqual("Бендикс стартера MMC - MD619865", siteResult.ModelName);
            Assert.AreEqual("svc=1&q=%3D%3Dqn2GtoXydrn1dDZvwDXvMC", workUnitResult.QueryString);
        }
    }
}

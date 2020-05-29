using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers._911Site.Models;
using ParserApi.Parsers._911Site.WorkUnits;
using ParserApi.Parsers.Site911Parser;
using SiteParsingHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserApi.Tests.MockedDataAcceptanceTests.Site911.WorkUnits
{
    [TestClass]
    public class D0parseHtmlPartDetailsTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var c0httpResponse = new C0httpResponse
            {
                Html = File.ReadAllText(@"MockedDataAcceptanceTests\Data\C0httpsResponse.html")
            };
            var siteResult = new Site911Result();
            var result = (ParserExecutorResultBase)siteResult;

            var workUnit = new D0parseHtmlPartDetails();
            var workUnitResult = workUnit.Execute(c0httpResponse, ref result);


        }
    }
}

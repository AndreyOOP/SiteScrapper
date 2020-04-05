using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers._911Site.Models;
using ParserApi.Parsers._911Site.WorkUnits;
using ParserApi.Parsers.Site911Parser;
using SiteParsingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParserApi.Tests.RealDataAcceptanceTests._911Site.WorkUnits
{
    [TestClass]
    public class C0httpRequestPartDetailsTest
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var httpClient = new HttpClient();

            var workUnit = new C0httpRequestPartDetails(httpClient);
            var inModel = new B0parseHtmlResult { QueryString = "svc=1&q=%3D%3Dqn2GtoXydrn1dDZvwDXvMC" };
            var result = (ParserExecutorResultBase)new Site911Result();

            var c0Result = (C0httpResponse)workUnit.Execute(inModel, ref result);

            Assert.IsNull(result.Exception);
            Assert.IsTrue(c0Result.Html.Length > 0);
        }
    }
}

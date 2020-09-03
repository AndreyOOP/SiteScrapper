using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class HtmlStep1ToQueryStringStep2Tests
    {
        HtmlStep1ToQueryStringStep2 worker;

        [TestInitialize]
        public void Initialize()
        {
            worker = new HtmlStep1ToQueryStringStep2();
        }

        [TestMethod]
        [DataRow(@"UnitTests\Site911\TestFiles\PrimaryTableExists.html")]
        public void IsExecute(string path)
        {
            var htmlStep1 = new HtmlStep1
            {
                Html = File.ReadAllText(path)
            };

            Assert.IsTrue(worker.IsExecutable(htmlStep1));
        }

        [TestMethod]
        [DataRow(@"UnitTests\Site911\TestFiles\PrimaryTableExists.html")]
        public void Parse(string path)
        {
            var htmlStep1 = new HtmlStep1
            {
                Html = File.ReadAllText(path)
            };

            var result = worker.Parse(htmlStep1);

            Assert.AreEqual("==qn2GtoXydrn1dDZvwDXvMC", result.Value);
        }
    }
}

using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Autoklad.WorkUnits;
using System.IO;
using System.Net.Http;

namespace Investigation
{
    [TestClass]
    public class AutokladInvestigation
    {
        [Ignore]
        [TestMethod]
        public void GetDataFromWebRequest()
        {
            var step1 = new InToHtmlS1AK(new HttpClient());
            var http = step1.Parse(new InAK { Id = "MR984375" });

            var step2 = new HtmlToFirstResultAK();
            var result = step2.Parse(http);
        }

        [TestMethod]
        public void GetDataFromFile()
        {
            var htmlDocument = new HtmlDocument();
            var html = File.ReadAllText(@"Investigation\Autoklad\HtmlStep1.html");
            htmlDocument.LoadHtml(html);

            var step2 = new HtmlToFirstResultAK();
            var result = step2.Parse(new HtmlS1AK { Html = html });

            Assert.AreEqual(4, result.FirstResult.Count);
        }
    }
}

using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Extensions;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Autoklad.WorkUnits;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Investigation
{
    [TestClass]
    public class AutokladInvestigation
    {
        [Ignore]
        [TestMethod]
        public void Step3FromWebRequest()
        {
            var httpClient = new HttpClient();
            var searchUri = new Uri(new Uri("https://www.autoklad.ua/"), "buy/toyota_MR984375");
            var response = httpClient.GetAsync(searchUri).Result;
            var html = response.GetContent();

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var analog = document.DocumentNode.SelectSingleNode("//h5[@id='aanalog']");  // the same parse way but asovpad
            var table = analog.ParentNode.ParentNode.SelectSingleNode(".//div[@id='table_div']");
            var trs = table.SelectNodes(".//tr[@id='tr']").ToArray();

            var trs2 = analog.ParentNode.ParentNode.SelectNodes(".//div[@id='table_div']//tr[@id='tr']");

            // note depend on city - table could contain few pages
            // it is possible to do addition request, for first version it is ok
            var res = trs2.Select(t => new
            {
                a = t.SelectSingleNode(".//td[1]/a").InnerText,
                b = t.SelectSingleNode(".//td[2]").InnerText,
                c = t.SelectSingleNode(".//td[3]").InnerText,
            }).ToArray();
        }

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

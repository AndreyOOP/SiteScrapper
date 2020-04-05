using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace ParserApi.Tests.FeatureTest
{
    [TestClass]
    public class HtmlParserUsage
    {
        private HtmlDocument htmlDoc;

        [TestInitialize]
        public void Initialize()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"MockedDataAcceptanceTests\Data\A0httpResponse.html");
            var html = File.ReadAllText(path);

            htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);
        }

        // Sample how to get file from project, load html, select first div with id = 'product_list'
        [TestMethod]
        public void SelectSingleNodeSample()
        {
            var divElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='product_list']");
            Console.WriteLine(divElement.InnerHtml); //divElement.SelectSingleNode("//table/tr[@class='hl']/td/a").InnerText
        }

        [TestMethod]
        public void CutQuery()
        {
            var script = htmlDoc.DocumentNode.SelectSingleNode("//script[@type='text/javascript']//text()[contains(., 'zakaz_blk_svc')]");

            var from = script.InnerText.IndexOf("svc=1&q=");
            var firstPart = script.InnerText.Substring(from);
            var to = firstPart.IndexOf("'");

            var query = firstPart.Substring(0, to);

            Assert.AreEqual("svc=1&q=%3D%3Dqn2GtoXydrn1dDZvwDXvMC", query);
        }
    }
}

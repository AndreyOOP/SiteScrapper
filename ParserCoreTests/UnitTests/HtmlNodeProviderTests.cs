using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore.Models.HtmlNodeProviders;

namespace UnitTests
{
    [TestClass]
    public class HtmlNodeProviderTests
    {
        [TestMethod]
        public void HtmlNodeFromNodeProvider_CreateProvider_HtmlNode()
        {
            var html = "<head></head>";
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var nodeProvider = new HtmlNodeFromNodeProvider(htmlDocument.DocumentNode);

            Assert.AreEqual(typeof(HtmlNode), nodeProvider.Node.GetType());
        }

        [TestMethod]
        public void HtmlNodeFromHtmlProvider_CreateProvider_HtmlNode()
        {
            var nodeProvider = new HtmlNodeFromHtmlProvider("<head></head>");

            Assert.AreEqual(typeof(HtmlNode), nodeProvider.Node.GetType());
        }

        [TestMethod]
        public void HtmlNodeFromHtmlProvider_IncorrectHtml_()
        {
            var nodeProvider = new HtmlNodeFromHtmlProvider("not html");

            Assert.AreEqual(typeof(HtmlNode), nodeProvider.Node.GetType());
        }
    }

}

using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Exceptions;
using ParserApi.Extensions;

namespace ParserApi.Tests.UnitTests.Extensions
{
    [TestClass]
    public class HtmlNodeExtensionsTests
    {
        HtmlDocument htmlDocument;

        [TestInitialize]
        public void Initialize()
        {
            htmlDocument = new HtmlDocument();
        }

        [TestMethod]
        [DataRow(" - ", false)]
        [DataRow("/style/2/minus.gif", false)]
        [DataRow("/style/2/plus.gif", true)]
        public void GetSign_CorrectAttribute_AsExpected(string val, bool expected)
        {
            htmlDocument.LoadHtml($"<th><img title='Нет в наличии' alt='нет' src='{val}' /></th>");
            var testNode = htmlDocument.DocumentNode.FirstChild;

            Assert.AreEqual(expected, testNode.GetSign());
        }

        [TestMethod]
        [DataRow("src=''")]
        [DataRow("")]
        public void GetSign_MissingOrIncorrectSrc_ParsingException(string val)
        {
            htmlDocument.LoadHtml($"<th><img title='Нет в наличии' alt='нет' {val} /></th>");
            var testNode = htmlDocument.DocumentNode.FirstChild;

            Assert.ThrowsException<ParsingException>(() => testNode.GetSign());
        }
    }
}

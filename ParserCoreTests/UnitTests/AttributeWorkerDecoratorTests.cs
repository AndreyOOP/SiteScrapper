using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore;
using ParserCore.Attributes;
using ParserCore.Models.HtmlNodeProviders;

namespace UnitTests
{
    [TestClass]
    public class AttributeWorkerDecoratorTests
    {
        class TIn : HtmlNodeFromHtmlProvider
        {
            public TIn(string html) : base(html) { }
        }

        class XPathTestClass
        {
            [XPath("//div")]
            public string FirstDivFromRoot { get; set; }

            [XPath("//div[@class='container']")]
            public string DivWithClassContainer { get; set; }

            [XPath("//div[@class='not-existed-class']")]
            public string NotFound { get; set; }

            // if you need apply function to xPath result:
            private string ink;
            [XPath("//a")]
            public string Ink
            {
                get => ink.Substring(1);
                set => ink = value;
            }

            [XPath("./html/body/header/nav/div[last()]")]
            public string DivInNav { get; set; }
        }

        class XPathTestWorker : IWorker<TIn, XPathTestClass>
        {
            public bool IsExecutable(TIn model) => true;

            public XPathTestClass Parse(TIn model) => new XPathTestClass();
        }

        [TestMethod]
        [DataRow(@"
        <html>
            <head>
                <meta charset='utf-8'>
            </head>
            <body>
                <header>
                    <nav id='header-nav' class='navbar navbar-default'>
                        <div class='container'>About Container</div>
                        <div>
                            <h1>Header</h1>
                            <a href='http://link.com'>Link</a>
                        </div>
                    </nav>
                </header>
                <div id='#id_div'></div>
            </body>
        </html>")]
        public void Parse_TypeWithXPathAttributes_ValuesAsExpected(string html)
        {
            var attributeWorkerDecorator = new AttributeWorkerDecorator<TIn, XPathTestClass>(new XPathTestWorker());

            var result = attributeWorkerDecorator.Parse(new TIn(html));

            Assert.AreEqual("About Container", result.FirstDivFromRoot);
            Assert.AreEqual("About Container", result.DivWithClassContainer);
            Assert.IsNull(result.NotFound);
            Assert.AreEqual("ink", result.Ink);
            StringAssert.Contains(result.DivInNav, "Header");
        }
    }

}

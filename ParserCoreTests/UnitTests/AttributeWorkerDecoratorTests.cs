using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCore;
using ParserCore.Attributes;
using ParserCore.Models.HtmlNodeProviders;
using System;

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

    [TestClass]
    public class AttributeWorkerDecoratorTypesTests
    {
        class TIn : HtmlNodeFromHtmlProvider
        {
            public TIn(string html) : base(html) { }
        }

        class XPathTypesTestClass
        {
            [XPath("//div[@id='int']")]
            public int Int { get; set; }

            [XPath("//div[@id='long']")]
            public long Long { get; set; }

            [XPath("//div[@id='bool-true']")]
            public bool True { get; set; }

            [XPath("//div[@id='bool-false']")]
            public bool False { get; set; }

            [XPath("//div[@id='bool-can-not-parse']")]
            public bool BoolCanNotParse { get; set; }

            [XPath("//div[@id='double']")]
            public double Double { get; set; }

            [XPath("//div[@id='decimal']")]
            public decimal Decimal { get; set; }
        }

        class XPathTypesTestWorker : IWorker<TIn, XPathTypesTestClass>
        {
            public bool IsExecutable(TIn model) => true;

            public XPathTypesTestClass Parse(TIn model) => new XPathTypesTestClass();
        }

        [TestMethod]
        [DataRow(@"
        <html>
            <div id='int'>123</div>
            <div id='long'>12345</div>
            <div id='bool-true'>True</div>
            <div id='bool-false'>false</div>
            <div id='bool-can-not-parse'>txt</div>
            <div id='double'>1.25</div>
            <div id='decimal'>5.123456789</div>
        </html>")]
        public void Parse_DifferentPropertyTypes_ValuesAsExpected(string html)
        {
            var attributeWorkerDecorator = new AttributeWorkerDecorator<TIn, XPathTypesTestClass>(new XPathTypesTestWorker());

            var result = attributeWorkerDecorator.Parse(new TIn(html));

            Assert.AreEqual(123, result.Int);
            Assert.AreEqual(12345, result.Long);
            Assert.AreEqual(true, result.True);
            Assert.AreEqual(false, result.False);
            Assert.AreEqual(default, result.BoolCanNotParse);
            Assert.AreEqual(1.25, result.Double);
            Assert.AreEqual(5.123456789m, result.Decimal);
        }

        
    }

    [TestClass]
    public class XPathAppliedToNotSupportedPropertyTypeTest
    {
        class TIn : HtmlNodeFromHtmlProvider
        {
            public TIn(string html) : base(html) { }
        }

        class XPathTypesTestClass
        {
            [XPath("//div[@id='uint']")]
            public uint Uint { get; set; }
        }

        class XPathTypesTestWorker : IWorker<TIn, XPathTypesTestClass>
        {
            public bool IsExecutable(TIn model) => true;

            public XPathTypesTestClass Parse(TIn model) => new XPathTypesTestClass();
        }

        [TestMethod]
        [DataRow(@"
        <html>
            <div id='uint'>123</div>
        </html>")]
        public void Parse_XPathAppliedToNotSupportedPropertyType_NotImplementedException(string html)
        {
            var attributeWorkerDecorator = new AttributeWorkerDecorator<TIn, XPathTypesTestClass>(new XPathTypesTestWorker());

            var exception = Assert.ThrowsException<NotImplementedException>(() => attributeWorkerDecorator.Parse(new TIn(html)));

            Assert.AreEqual("XPathAttribute is not implemented for type UInt32", exception.Message);
        }
    }
}

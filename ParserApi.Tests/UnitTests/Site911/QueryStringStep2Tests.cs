using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Site911.Models;

namespace ParserApi.UnitTests
{
    [TestClass]
    public class QueryStringStep2Tests
    {
        [TestMethod]
        [DataRow("svc=1&q=%3D%3Dqn2GtoXydrn1dDZvwDXvMC")]
        public void Value_FullQueryString_Ok(string fullQueryString)
        {
            var queryString = new QueryStringStep2(fullQueryString);

            Assert.AreEqual("==qn2GtoXydrn1dDZvwDXvMC", queryString.Value);
        }
    }
}

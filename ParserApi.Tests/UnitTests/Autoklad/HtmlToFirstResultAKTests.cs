using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Autoklad.WorkUnits;
using System.IO;
using System.Linq;

namespace ParserApi.Tests.UnitTests.Autoklad
{
    [TestClass]
    public class HtmlToFirstResultAKTests
    {
        HtmlToFirstResultAK step2;

        [TestInitialize]
        public void Initialize()
        {
            step2 = new HtmlToFirstResultAK();
        }

        [TestMethod]
        [DataRow(@"UnitTests\Autoklad\TestFiles\NoULNode.html")]
        public void Parse_NoUlNodeWithExcpectedClass_NoData(string path)
        {
            var inputModel = new HtmlS1AK { 
                Html = File.ReadAllText(path) 
            };
            var result = step2.Parse(inputModel);

            Assert.IsNull(result.FirstResult);
        }

        [TestMethod]
        [DataRow(@"UnitTests\Autoklad\TestFiles\LiItem.html")]
        public void Parse_NoBrandModelPrice_NullsOnMissingData(string path)
        {
            var inputModel = new HtmlS1AK
            {
                Html = File.ReadAllText(path)
            };
            var result = step2.Parse(inputModel).FirstResult.First();

            Assert.IsNull(result.Brand);
            Assert.IsNull(result.Model);
            Assert.IsNull(result.Price);
        }

        [TestMethod]
        [DataRow(@"UnitTests\Autoklad\TestFiles\MissingHref.html")]
        public void Parse_NoHref_(string path)
        {
            var inputModel = new HtmlS1AK
            {
                Html = File.ReadAllText(path)
            };
            var result = step2.Parse(inputModel).FirstResult.First();

            Assert.IsNull(result.PartBrandLink);
        }
    }
}

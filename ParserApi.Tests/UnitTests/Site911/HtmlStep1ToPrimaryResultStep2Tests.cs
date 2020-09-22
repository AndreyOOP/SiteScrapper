using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class HtmlStep1ToPrimaryResultStep2Tests
    {
        HtmlStep1ToPrimaryResultStep2 worker;

        [TestInitialize]
        public void Initialize()
        {
            worker = new HtmlStep1ToPrimaryResultStep2();
        }

        [TestMethod]
        [DataRow(@"UnitTests\Site911\TestFiles\PrimaryTableExists.html")]
        public void IsExecutable_PrimaryTableExist_True(string path)
        {
            var htmlStep1 = new HtmlStep1
            {
                Html = File.ReadAllText(path)
            };

            Assert.IsTrue(worker.IsExecutable(htmlStep1));
        }

        [TestMethod]
        [DataRow(@"UnitTests\Site911\TestFiles\PrimaryTableExists.html")]
        public void Parsing(string path)
        {
            var htmlStep1 = new HtmlStep1
            {
                Html = File.ReadAllText(path)
            };

            var result = worker.Parse(htmlStep1);

            Assert.AreEqual("Бендикс стартера MMC - MD619865", result.Name);
            Assert.IsFalse(result.InStockDnepr ?? false);
            Assert.IsTrue(result.InStockKiev ?? false);
            Assert.IsTrue(result.KievOneDay ?? false);
            Assert.AreEqual(1032, result.Price);
        }
    }
}

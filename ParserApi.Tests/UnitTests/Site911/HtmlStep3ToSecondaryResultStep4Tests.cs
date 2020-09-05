using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using System.IO;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class HtmlStep3ToSecondaryResultStep4Tests
    {
        HtmlStep3ToSecondaryResultStep4 worker;

        [TestInitialize]
        public void Initialize()
        {
            worker = new HtmlStep3ToSecondaryResultStep4();
        }

        [TestMethod]
        [DataRow(@"UnitTests\Site911\TestFiles\SecondaryTable.html")]
        public void Parse_SecondaryTableHtml_AsExpected(string path)
        {
            var htmlStep3 = new HtmlStep3
            {
                Html = File.ReadAllText(path)
            };

            var result = worker.Parse(htmlStep3);

            Assert.AreEqual(4, result.Table.Count());
            var testRow = result.Table.ToArray()[1];
            Assert.AreEqual("MITSUBISHI", testRow.Brand);
            Assert.AreEqual("20-24 дней ", testRow.DeliveryDays);
            Assert.AreEqual("БЕНДИКС СТАРТЕРА / CLUTCH,STARTER OVERR", testRow.Name);
            Assert.AreEqual("MD619865", testRow.PartId);
            Assert.AreEqual(952.0, testRow.PriceUah);
            Assert.AreEqual(20, testRow.Qty);
            Assert.AreEqual("E 1-2 d.", testRow.Storage);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("zzz")]
        [DataRow("<td></td>")]
        public void Parse_UnexpectedHtml_NullTable(string html)
        {
            var htmlStep3 = new HtmlStep3
            {
                Html = html
            };
            var result = worker.Parse(htmlStep3);

            Assert.IsNull(result.Table);
        }
    }
}

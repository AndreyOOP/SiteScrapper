using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Controllers;
using ParserApi.Parsers.Site911.Models;

namespace AcceptanceTests
{
    [TestClass]
    public class ParseSingleModel
    {
        [Ignore("Required for investigation")]
        [TestMethod]
        [DataRow("MD619865", true, true)]
        [DataRow("MD619866", false, true)]
        [DataRow("MD619867", false, false)]
        public void ParseSingleModel_ModelWithPrimaryAndSecondaryData(string partId, bool primaryTableExist, bool secondaryTableExist)
        {
            var controller = new ParsingController();

            var result = (Result)controller.ParseSingleModel(partId);

            if(primaryTableExist)
                Assert.IsNotNull(result.Primary);

            if(secondaryTableExist)
                Assert.IsNotNull(result.Secondary);
        }
    }
}

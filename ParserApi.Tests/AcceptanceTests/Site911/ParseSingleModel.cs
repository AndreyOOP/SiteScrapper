using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi.Controllers;
using ParserApi.Parsers.Site911.Models;
using ParserCore;

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
            // ToDo: how to resolve controller here - ?
            var controller = new ParsingController(new ExceptionLoggerSettings(), new InMemoryWorkerLogger());

            var result = (Result)controller.ParseSingleModel(partId);

            if(primaryTableExist)
                Assert.IsNotNull(result.Primary);

            if(secondaryTableExist)
                Assert.IsNotNull(result.Secondary);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserApi;
using ParserApi.Controllers;
using ParserApi.Controllers.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore;
using ParserCore.Abstraction;
using Unity;

namespace AcceptanceTests
{
    [TestClass]
    public class ParseSingleModel
    {
        IUnityContainer container;

        [TestInitialize]
        public void Initialize()
        {
            container = UnityConfig.RegisterComponents();
            
            // it is necessary to override types registered as PerRequestLifetimeManager because this time manager works only with http requests
            container.RegisterType<IInMemoryWorkerLogger, InMemoryWorkerLogger>(TypeLifetime.Singleton); 
            container.RegisterType<WorkerLogSettings, ExceptionLoggerSettings>(TypeLifetime.Singleton);
        }

        [Ignore("Required for investigation")]
        [TestMethod]
        [DataRow("MD619865", true, true)]
        [DataRow("MD619866", false, true)]
        [DataRow("MD619867", false, false)]
        public void ParseSingleModel_ModelWithPrimaryAndSecondaryData(string partId, bool primaryTableExist, bool secondaryTableExist)
        {
            var controller = new ParsingController(container.Resolve<IInMemoryWorkerLogger>(), container.Resolve<Site911Parser>());

            var result = (Result)controller.ParseSingleModel(partId, new RequestParams());

            if(primaryTableExist)
                Assert.IsNotNull(result.Primary);

            if(secondaryTableExist)
                Assert.IsNotNull(result.Secondary);
        }
    }
}

using HtmlAgilityPack;
using ParserApi.Parsers.Site911Demo.Models;
using ParserApi.Parsers.Site911Demo.WorkUnits;
using ParserApi.Parsers.Site911EventBusPrototype.Models;
using ParserApi.Parsers.Site911EventBusPrototype.WorkUnits;
//using ParserCoreProject.ParserCore;
using System.Net.Http;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("api/event/{id}")]
        public Site911ParsingResult GetEventBusImplementation([FromUri]string id)
        {
            //var httpClient = new HttpClient();
            //var parser = new ParserCoreProject.ParserCore.WebParser<Site911ParsingResult>();

            //parser.RegisterUnit<A_SearchModelByIdRequestInput, A_SearchModelByIdRequestOutput>(new A_SearchModelByIdRequest(parser, httpClient));
            //parser.RegisterUnit<A_SearchModelByIdRequestOutput, B_ParseHtmlOutput>(new B_ParseHtml(parser, new HtmlDocument()));
            //parser.RegisterUnit<B_ParseHtmlOutput, C_QueryResultOutput>(new C_RequestPartDetails(parser, httpClient));
            //parser.RegisterUnit<C_QueryResultOutput, Site911ParsingResult>(new D_ParseHtmlPartDetails(parser, new HtmlDocument()));

            //parser.ExecuteUnit<A_SearchModelByIdRequestInput, A_SearchModelByIdRequestOutput>(new A_SearchModelByIdRequestInput { Id = id });

            //return parser.Result;
            return null;
        }

        [HttpGet]
        [Route("api/core/{id}")]
        public Result5 GetResultParserCore([FromUri]string id)
        {
            //var httpClient = new HttpClient();

            //var workersContainer = new WorkersContainer<In1, Html2>();
            //var workerPreprocessorsContainer = new WorkerPreprocessorsContainer();
            //var workerSharedServices = new WorkerSharedServices<In1, Html2, Result5>(workersContainer, workerPreprocessorsContainer, new Result5());

            //workersContainer.Add(new Step1(workerSharedServices, httpClient));
            //workersContainer.Add(new Step2(workerSharedServices, new HtmlDocument()));
            //workersContainer.Add(new Step3(workerSharedServices, httpClient));
            //workersContainer.Add(new Step4(workerSharedServices, new HtmlDocument()));

            //workersContainer.GetFirst().ParseAndExecuteNext(new In1 { Id = id });

            //return workerSharedServices.Result;
            return null;
        }
    }
}

using HtmlAgilityPack;
using ParserApi.Parsers.Site911EventBusPrototype.Models;
using ParserApi.Parsers.Site911EventBusPrototype.WorkUnits;
using ParserCoreProject.ParserCore;
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
            var httpClient = new HttpClient();
            var parser = new ParserCoreProject.ParserCore.WebParser<Site911ParsingResult>();

            parser.RegisterUnit<A_SearchModelByIdRequestInput, A_SearchModelByIdRequestOutput>(new A_SearchModelByIdRequest(parser, httpClient));
            parser.RegisterUnit<A_SearchModelByIdRequestOutput, B_ParseHtmlOutput>(new B_ParseHtml(parser, new HtmlDocument()));
            parser.RegisterUnit<B_ParseHtmlOutput, C_QueryResultOutput>(new C_RequestPartDetails(parser, httpClient));
            parser.RegisterUnit<C_QueryResultOutput, Site911ParsingResult>(new D_ParseHtmlPartDetails(parser, new HtmlDocument()));

            parser.ExecuteUnit<A_SearchModelByIdRequestInput, A_SearchModelByIdRequestOutput>(new A_SearchModelByIdRequestInput { Id = id });

            return parser.Result;
        }
    }
}

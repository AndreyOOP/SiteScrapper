using HtmlAgilityPack;
using ParserApi.Parsers._911Site.WorkUnits;
using ParserApi.Parsers.Site911.WorkUnits;
using ParserApi.Parsers.Site911EventBusPrototype.Models;
using ParserApi.Parsers.Site911EventBusPrototype.WorkUnits;
using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using ParserApi.Parsers.Site911Parser.WorkUnits.Models;
using SiteParsingHelper.Event;
using SiteParsingHelper.Tree;
using System.Net.Http;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("api/{id}")]
        public Site911Result Get([FromUri]string id)
        {
            var httpClient = new HttpClient();
            var tree = new WorkUnitTree(new A0httpRequest(httpClient));
                           tree.AddNextNode(new WorkUnitTree(new B0parseHtml()))
                           .AddNextNode(new WorkUnitTree(new C0httpRequestPartDetails(httpClient)))
                           .AddNextNode(new WorkUnitTree(new D0parseHtmlPartDetails()));

            var parser = new Site911Parser(tree);

            var result = parser.Parse(new Input { Id = id });

            return result;
        }

        [HttpGet]
        [Route("api/event/{id}")]
        public Site911ParsingResult GetEventBusImplementation([FromUri]string id)
        {
            var httpClient = new HttpClient();
            var parser = new WebParser<Site911ParsingResult>();

            parser.RegisterUnit<A_SearchModelByIdRequestInput, A_SearchModelByIdRequestOutput>(new A_SearchModelByIdRequest(parser, httpClient));
            parser.RegisterUnit<A_SearchModelByIdRequestOutput, B_ParseHtmlOutput>(new B_ParseHtml(parser, new HtmlDocument()));
            parser.RegisterUnit<B_ParseHtmlOutput, C_QueryResultOutput>(new C_RequestPartDetails(parser, httpClient));
            parser.RegisterUnit<C_QueryResultOutput, Site911ParsingResult>(new D_ParseHtmlPartDetails(parser, new HtmlDocument()));

            parser.ExecuteUnit<A_SearchModelByIdRequestInput, A_SearchModelByIdRequestOutput>(new A_SearchModelByIdRequestInput { Id = id });

            return parser.Result;
        }
    }
}

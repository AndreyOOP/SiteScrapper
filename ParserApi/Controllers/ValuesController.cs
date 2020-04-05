using ParserApi.Parsers._911Site.WorkUnits;
using ParserApi.Parsers.Site911.WorkUnits;
using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.Models;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using ParserApi.Parsers.Site911Parser.WorkUnits.Models;
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
    }
}

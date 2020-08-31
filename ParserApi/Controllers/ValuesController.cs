using HtmlAgilityPack;
using ParserApi.Parsers.Site911ParserCore;
using ParserApi.Parsers.Site911ParserCore.Models;
using ParserApi.Parsers.Site911ParserCore.Workers;
using ParserCore;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("api/core/{id}")]
        public Result GetResultParserCore([FromUri]string id)
        {
            var defaultSettings = new DefaultSettings();
            var memoryLogger = new InMemoryWorkerLogger();

            var parsingGraph = new Dictionary<IInOutKey, object>
            {
                [new InOutKey<In, HtmlSearchResult2>()] = new LoggingWorker<In, HtmlSearchResult2>(new InToHtmlSearchResult(new HttpClient()), memoryLogger, defaultSettings),
                [new InOutKey<HtmlSearchResult2, StringQuery3>()] = new LoggingWorker<HtmlSearchResult2, StringQuery3>(new HtmlSearchResultToStringQuery(new HtmlDocument()), memoryLogger, defaultSettings),
                [new InOutKey<HtmlSearchResult2, OutModelName3>()] = new HtmlSearchResultToOutModelName(new HtmlDocument()),
                [new InOutKey<StringQuery3, HtmlQueryResult4>()] = new StringQueryToHtmlQueryResult(new HttpClient()),
                [new InOutKey<HtmlQueryResult4, OutTable5>()] = new HtmlQueryResultToOutTable(new HtmlDocument()),
            };
            var workersContainer = new WorkersContainer(parsingGraph);
            var parser = new Site911Parser(workersContainer);

            var inModel = new In 
            {
                Id = id
            };
            var result = parser.Parse(inModel);

            result.logger = memoryLogger;

            return result;
        }
    }
}

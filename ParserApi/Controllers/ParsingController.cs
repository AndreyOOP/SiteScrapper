using HtmlAgilityPack;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using ParserApi.Parsers.Site911ParserCore;
using ParserApi.Parsers.Site911ParserCore.Models;
using ParserApi.Parsers.Site911ParserCore.Workers;
using ParserCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ParsingController : ApiController
    {
        [HttpGet]
        [Route("api/core/{id}")]
        public ResultPrev GetResultParserCore([FromUri]string id)
        {
            var defaultSettings = new DefaultSettings();
            var memoryLogger = new InMemoryWorkerLogger();

            var parsingGraph = new Dictionary<IInOutKey, object>
            {
                [new InOutKey<InPrev, HtmlSearchResult2>()] = new LoggingWorker<InPrev, HtmlSearchResult2>(new InToHtmlSearchResult(new HttpClient()), memoryLogger, defaultSettings),
                [new InOutKey<HtmlSearchResult2, StringQuery3>()] = new LoggingWorker<HtmlSearchResult2, StringQuery3>(new HtmlSearchResultToStringQuery(new HtmlDocument()), memoryLogger, defaultSettings),
                [new InOutKey<HtmlSearchResult2, OutModelName3>()] = new HtmlSearchResultToOutModelName(new HtmlDocument()),
                [new InOutKey<StringQuery3, HtmlQueryResult4>()] = new StringQueryToHtmlQueryResult(new HttpClient()),
                [new InOutKey<HtmlQueryResult4, OutTable5>()] = new HtmlQueryResultToOutTable(new HtmlDocument()),
            };
            var workersContainer = new WorkersContainer(parsingGraph);
            var parser = new Site911ParserPrev(workersContainer);

            var inModel = new InPrev 
            {
                Id = id
            };
            var result = parser.Parse(inModel);

            result.logger = memoryLogger;

            return result;
        }

        [HttpGet]
        [Route("api/parse/{sparePartId}")]
        public object ParseSingleModel([FromUri]string sparePartId)
        {
            var defaultSettings = new DefaultSettings();
            var memoryLogger = new InMemoryWorkerLogger();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            
            var parsingGraph = new Dictionary<IInOutKey, object>
            {
                [new InOutKey<In, HtmlStep1>()] = new LoggingWorker<In, HtmlStep1>(new InToHtmlStep1(new HttpClient()), memoryLogger, defaultSettings),
                [new InOutKey<HtmlStep1, PrimaryResultStep2>()] = new LoggingWorker<HtmlStep1, PrimaryResultStep2>(new HtmlStep1ToPrimaryResultStep2(), memoryLogger, defaultSettings),
                [new InOutKey<HtmlStep1, QueryStringStep2>()] = new LoggingWorker<HtmlStep1, QueryStringStep2>(new HtmlStep1ToQueryStringStep2(), memoryLogger, defaultSettings),
                [new InOutKey<QueryStringStep2, HtmlStep3>()] = new LoggingWorker<QueryStringStep2, HtmlStep3>(new QueryStringStep2ToHtmlStep3(new HttpClient()), memoryLogger, defaultSettings),
            };
            var workersContainer = new WorkersContainer(parsingGraph);
            var parser = new Site911Parser(workersContainer);

            var inModel = new In
            {
                Id = sparePartId
            };
            var result = parser.Parse(inModel);
            return result;
        }
    }
}

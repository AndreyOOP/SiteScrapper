using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore;
using ParserCore.Abstraction;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ParsingController : ApiController
    {
        private WorkerLogSettings parseSettings;
        private ILogger<WorkerLogRecord> memoryLogger;

        public ParsingController(WorkerLogSettings workerLogSettings, ILogger<WorkerLogRecord> logger)
        {
            parseSettings = workerLogSettings;
            memoryLogger = logger;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        [HttpGet]
        [Route("api/parse/{sparePartId}")]
        public object ParseSingleModel([FromUri]string sparePartId)
        {
            
            var parsingGraph = new Dictionary<IInOutKey, object>
            {
                [new InOutKey<In, HtmlStep1>()] = new LoggingWorker<In, HtmlStep1>(new InToHtmlStep1(new HttpClient()), memoryLogger, parseSettings),
                [new InOutKey<HtmlStep1, PrimaryResultStep2>()] = new LoggingWorker<HtmlStep1, PrimaryResultStep2>(new HtmlStep1ToPrimaryResultStep2(), memoryLogger, parseSettings),
                [new InOutKey<HtmlStep1, QueryStringStep2>()] = new LoggingWorker<HtmlStep1, QueryStringStep2>(new HtmlStep1ToQueryStringStep2(), memoryLogger, parseSettings),
                [new InOutKey<QueryStringStep2, HtmlStep3>()] = new LoggingWorker<QueryStringStep2, HtmlStep3>(new QueryStringStep2ToHtmlStep3(new HttpClient()), memoryLogger, parseSettings),
                [new InOutKey<HtmlStep3, SecondaryResultStep4>()] = new LoggingWorker<HtmlStep3, SecondaryResultStep4>(new HtmlStep3ToSecondaryResultStep4(), memoryLogger, parseSettings),
            };
            var workersContainer = new WorkersContainer(parsingGraph);
            var parser = new Site911Parser(workersContainer);

            var inModel = new In
            {
                Id = sparePartId
            };

            var result = new Result();
            try
            {
                result = parser.Parse(inModel);
                
            }
            catch (System.Exception)
            {
            }
            result.Log = ((InMemoryWorkerLogger)memoryLogger).Records; // to fix
            return result;
        }
    }
}

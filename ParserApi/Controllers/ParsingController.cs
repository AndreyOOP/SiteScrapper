using ParserApi.Controllers.Models;
using ParserApi.Parsers.Autoklad;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore;
using System.Net;
using System.Web.Http;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ParserApi.Controllers
{
    public class ParsingController : ApiController
    {
        private Site911Parser site911Parser;
        private AutokladParser autokladParser;

        public ParsingController(Site911Parser site911Parser, AutokladParser autokladParser)
        {
            this.site911Parser = site911Parser;
            this.autokladParser = autokladParser;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        [HttpPost]
        [Route("api/parse/{sparePartId}")]
        public object ParseSingleModel([FromUri]string sparePartId, [FromBody]RequestParams @params)
        {
            var result911 = GetParsingResult(site911Parser, new In911 { Id = sparePartId });
            var resultAK = GetParsingResult(autokladParser, new InAK { Id = sparePartId });

            result911.Log = @params.ShowLog ? site911Parser.WorkerLogger.Records : site911Parser.WorkerLogger.ErrorRecords;
            resultAK.Log = @params.ShowLog ? autokladParser.WorkerLogger.Records : autokladParser.WorkerLogger.ErrorRecords;

            var json = JsonConvert.SerializeObject(new
            {
                Site911 = result911.RawData,
                Autoklad = resultAK.RawData
            }, Formatting.Indented);

            var result = new OutputSummaryWithLog
            {
                Parts = result911.Parts?.Concat(resultAK.Parts ?? new List<Part>()),
                PerRequestParts = result911.PerRequestParts?.Concat(resultAK.PerRequestParts ?? new List<PerRequestPart>()),
                AnalogParts = result911.AnalogParts?.Concat(resultAK.AnalogParts ?? new List<AnalogPart>()),
                MatchingParts = result911.MatchingParts?.Concat(resultAK.MatchingParts ?? new List<MatchingPart>()),
                Json = json,
                Site911Log = result911.Log,
                AutokladLog = resultAK.Log
            };
            return result;
        }

        private TOut GetParsingResult<TIn, TOut>(ParserBase<TIn, TOut> parser, TIn model)
        {
            var result = default(TOut);
            try
            {
                result = parser.Parse(model);
            }
            catch(Exception ex) // supperss parser exception as all of them catched by logger
            {
            }
            return result;
        }
    }
}

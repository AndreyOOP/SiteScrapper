using ParserApi.Controllers.Models;
using ParserApi.Parsers.Autoklad;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore;
using System.Net;
using System.Web.Http;

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

        [HttpGet]
        [Route("api/parse/{sparePartId}")]
        public object ParseSingleModel([FromUri]string sparePartId, [FromBody]RequestParams @params)
        {
            var result911 = GetParsingResult(site911Parser, new In { Id = sparePartId });
            var resultAK = GetParsingResult(autokladParser, new InAK { Id = sparePartId });

            result911.Log = @params.ShowLog ? site911Parser.WorkerLogger.Records : site911Parser.WorkerLogger.ErrorRecords;
            resultAK.Log = @params.ShowLog ? autokladParser.WorkerLogger.Records : autokladParser.WorkerLogger.ErrorRecords;

            return new ParsersResult
            {
                Site911 = result911,
                Autoklad = resultAK
            };
        }

        private TOut GetParsingResult<TIn, TOut>(ParserBase<TIn, TOut> parser, TIn model)
        {
            var result = default(TOut);
            try
            {
                result = parser.Parse(model);
            }
            catch // supperss parser exception as all of them catched by logger
            {
            }
            return result;
        }
    }
}

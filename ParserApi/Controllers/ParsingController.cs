using ParserApi.Controllers.Models;
using ParserApi.Parsers.Autoklad;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore.Abstraction;
using System.Net;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ParsingController : ApiController
    {
        private Site911Parser site911Parser;
        private AutokladParser autokladParser;
        private IInMemoryWorkerLogger memoryLogger911;
        private IInMemoryWorkerLogger memoryLoggerAK;

        public ParsingController(IInMemoryWorkerLogger memoryLogger911, IInMemoryWorkerLogger memoryLoggerAK, Site911Parser site911Parser, AutokladParser autokladParser)
        {
            this.site911Parser = site911Parser;
            this.autokladParser = autokladParser;

            this.memoryLogger911 = memoryLogger911;
            this.memoryLoggerAK = memoryLoggerAK;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        [HttpGet]
        [Route("api/parse/{sparePartId}")]
        public object ParseSingleModel([FromUri]string sparePartId, [FromBody]RequestParams @params)
        {
            var result911 = new Result();
            var resultAK = new ResultAK();

            try
            {
                result911 = site911Parser.Parse(new In { Id = sparePartId });
            }
            finally
            {
                result911.Log = @params.ShowLog ? memoryLogger911.Records : memoryLogger911.ErrorRecords;
            }

            try
            {
                resultAK = autokladParser.Parse(new InAK { Id = sparePartId });
            }
            finally
            {
                resultAK.Log = @params.ShowLog ? memoryLoggerAK.Records : memoryLoggerAK.ErrorRecords;
            }

            return new Summary
            {
                Site911Result = result911,
                AutokladResult = resultAK
            };
        }

        public class Summary
        {
            public Result Site911Result { get; set; }
            public ResultAK AutokladResult { get; set; }
        }
    }
}

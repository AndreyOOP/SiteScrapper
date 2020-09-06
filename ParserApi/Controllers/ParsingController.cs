using ParserApi.Controllers.Models;
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
        private IInMemoryWorkerLogger memoryLogger;

        public ParsingController(IInMemoryWorkerLogger memoryLogger, Site911Parser site911Parser)
        {
            this.site911Parser = site911Parser;
            this.memoryLogger = memoryLogger;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        [HttpGet]
        [Route("api/parse/{sparePartId}")]
        public object ParseSingleModel([FromUri]string sparePartId, [FromBody]RequestParams @params)
        {
            var result = new Result();

            try
            {
                result = site911Parser.Parse(new In { Id = sparePartId });
            }
            finally
            {
                result.Log = @params.ShowLog ? memoryLogger.Records : memoryLogger.ErrorRecords;
            }

            return result;
        }
    }
}

﻿using ParserApi.Controllers.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore;
using ParserCore.Abstraction;
using System.Net;
using System.Web.Http;

namespace ParserApi.Controllers
{
    public class ParsingController : ApiController
    {
        private Site911Parser site911Parser;
        private ILogger<WorkerLogRecord> memoryLogger;


        public ParsingController(ILogger<WorkerLogRecord> logger, Site911Parser site911Parser)
        {
            this.site911Parser = site911Parser;
            memoryLogger = logger;

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
                var logger = (IInMemoryWorkerLogger)memoryLogger;

                if (@params.showLog)
                    result.Log = logger.Records;
                else
                    result.Log = logger.ErrorRecords;
            }

            return result;
        }
    }
}

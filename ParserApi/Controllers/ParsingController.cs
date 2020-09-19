using ParserApi.Controllers.Models;
using ParserApi.Parsers;
using ParserApi.Parsers.Autoklad;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911ParserCore;
using ParserCore;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Linq;

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

            var result = Create(result911, resultAK, sparePartId);
            result.Site911Log = result911.Log;
            result.AutokladLog = resultAK.Log;
            return result;
        }

        // ToDo: move to service
        private ParsersResult Create(Result911 result911, ResultAK resultAK, string partId)
        {
            var mainTable = new List<MainPriceTableRow>();
            mainTable.Add(new MainPriceTableRow 
            { 
                ParserName = nameof(Parser.Site911),
                Name = result911?.Primary?.Name,
                Brand = "", // todo
                Price = result911?.Primary?.Price.ToString(),
                LinkToSource = $"https://911auto.com.ua/search/{partId}"
            });
            var resAK = resultAK.FirstResult?.FirstResult?.Select(r => new MainPriceTableRow
            {
                ParserName = nameof(Parser.Autoklad),
                Name = r?.Name,
                Brand = r?.Brand,
                Price = r?.Price,
                LinkToSource = $"https://www.autoklad.ua{r?.PartBrandLink}"
            });
            if(resAK != null)
                mainTable.AddRange(resAK);

            // ToDo: same + analogs + for request

            return new ParsersResult
            {
                Main = mainTable
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

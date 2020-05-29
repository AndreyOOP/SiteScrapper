using ParserApi.Parsers.Site911Parser.Models;
using ParserApi.Parsers.Site911Parser.WorkUnits.Models;
using SiteParsingHelper;
using SiteParsingHelper.Abstraction;
using System.Net;
using System.Net.Http;

namespace ParserApi.Parsers.Site911Parser.WorkUnits
{
    public class A0httpRequest : ISite911WorkUnit
    {
        private readonly HttpClient httpClient;

        public A0httpRequest(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            var inModel = (Input)input;

            var uri = $"http://911auto.com.ua/search/{inModel.Id}";
            var result = httpClient.GetAsync(uri).Result;

            if(result.StatusCode == HttpStatusCode.OK)
            {
                var html = result.Content.ReadAsStringAsync().Result;
                return new A0httpResponse { Html = html };
            }
            siteParserResult.Exception = "Get Request Status is Not 200OK";
            return null;
        }
    }
}
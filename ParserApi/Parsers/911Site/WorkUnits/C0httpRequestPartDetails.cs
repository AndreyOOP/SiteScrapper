using ParserApi.Parsers._911Site.Models;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using SiteParsingHelper;
using SiteParsingHelper.Abstraction;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ParserApi.Parsers._911Site.WorkUnits
{
    public class C0httpRequestPartDetails : ISite911WorkUnit
    {
        private readonly HttpClient httpClient;

        public C0httpRequestPartDetails(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            var b0parseHtmlResult = (B0parseHtmlResult)input;

            var body = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("svc", "1"),
                new KeyValuePair<string, string>("q", b0parseHtmlResult.QueryString)
            });

            var result = httpClient.PostAsync("https://911auto.com.ua", body).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var html = result.Content.ReadAsStringAsync().Result;
                return new C0httpResponse { Html = html };
            }
            siteParserResult.Exception = "Get Request Status is Not 200OK";
            return null;
        }
    }
}
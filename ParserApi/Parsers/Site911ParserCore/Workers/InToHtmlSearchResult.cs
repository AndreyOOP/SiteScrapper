using ParserApi.Parsers.Site911ParserCore.Models;
using System;
using System.Net;
using System.Net.Http;

namespace ParserApi.Parsers.Site911ParserCore.Workers
{
    public class InToHtmlSearchResult : WorkerBase<In, HtmlSearchResult2>
    {
        private readonly HttpClient httpClient;

        public InToHtmlSearchResult(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override HtmlSearchResult2 Parse(In model)
        {
            var uri = $"http://911auto.com.ua/search/{model.Id}"; // move to client setup - ?
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var result = httpClient.GetAsync(uri).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var html = result.Content.ReadAsStringAsync().Result;
                return new HtmlSearchResult2 
                { 
                    Html = html 
                };
            }
            throw new Exception("Get Request Status is Not 200OK");
        }
    }
}
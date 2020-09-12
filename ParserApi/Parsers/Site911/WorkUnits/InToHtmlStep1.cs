using ParserApi.Extensions;
using ParserApi.Parsers.Site911.Models;
using System;
using System.Net.Http;

namespace ParserApi.Parsers.Site911.WorkUnits
{
    public class In911ToHtmlStep1 : Site911WorkerBase<In911, HtmlStep1>
    {
        private readonly HttpClient httpClient;

        public In911ToHtmlStep1(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override HtmlStep1 Parse(In911 model)
        {
            var searchUri = new Uri(Site911Base, $"/search/{model.Id}");
            var response = httpClient.GetAsync(searchUri).Result;
            return new HtmlStep1
            {
                Html = response.GetContent()
            };
        }
    }
}
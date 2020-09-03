using ParserApi.Extensions;
using ParserApi.Parsers.Site911.Models;
using System;
using System.Net.Http;

namespace ParserApi.Parsers.Site911.WorkUnits
{
    public class InToHtmlStep1 : Site911WorkerBase<In, HtmlStep1>
    {
        private readonly HttpClient httpClient;

        public InToHtmlStep1(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override HtmlStep1 Parse(In model)
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
using ParserApi.Extensions;
using ParserApi.Parsers.Autoklad.Models;
using System;
using System.Net.Http;

namespace ParserApi.Parsers.Autoklad.WorkUnits
{
    public class InToHtmlS1AK : AutokladWorkerBase<InAK, HtmlS1AK>
    {
        private readonly HttpClient httpClient;

        public InToHtmlS1AK(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override HtmlS1AK Parse(InAK model)
        {
            var searchUri = new Uri(AutokladBase, $"/search/{model.Id}");
            var response = httpClient.GetAsync(searchUri).Result;
            return new HtmlS1AK
            {
                Html = response.GetContent() 
            };
        }
    }
}
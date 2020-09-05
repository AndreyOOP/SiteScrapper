using ParserApi.Extensions;
using ParserApi.Parsers.Site911.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace ParserApi.Parsers.Site911.WorkUnits
{
    public class QueryStringStep2ToHtmlStep3 : Site911WorkerBase<QueryStringStep2, HtmlStep3>
    {
        private readonly HttpClient httpClient;

        public QueryStringStep2ToHtmlStep3(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override HtmlStep3 Parse(QueryStringStep2 model)
        {
            var body = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("svc", "1"),
                new KeyValuePair<string, string>("q", model.Value)
            });
            var result = httpClient.PostAsync(Site911Base, body).Result;

            return new HtmlStep3
            {
                Html = result.GetContent()
            };
        }
    }
}
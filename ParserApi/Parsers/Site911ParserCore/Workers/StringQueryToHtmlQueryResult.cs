using ParserApi.Parsers.Site911ParserCore.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ParserApi.Parsers.Site911ParserCore.Workers
{
    public class StringQueryToHtmlQueryResult : WorkerBase<StringQuery3, HtmlQueryResult4>
    {
        private readonly HttpClient httpClient;

        public StringQueryToHtmlQueryResult(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override HtmlQueryResult4 Parse(StringQuery3 model)
        {
            var body = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("svc", "1"),
                new KeyValuePair<string, string>("q", model.QueryString)
            });

            var result = httpClient.PostAsync("https://911auto.com.ua", body).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var html = result.Content.ReadAsStringAsync().Result;
                return new HtmlQueryResult4 
                { 
                    Html = html 
                };
            }
            throw new Exception("Get Request Status is Not 200OK");
        }
    }
}
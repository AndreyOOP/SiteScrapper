using ParserApi.Parsers.Site911Demo.Models;
using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ParserApi.Parsers.Site911Demo.WorkUnits
{
    public class Step3 : WorkerBase<QueryString3, Html4, In1, Html2, Result5>
    {
        private readonly HttpClient httpClient;

        public Step3(IWorkerSharedServices<In1, Html2, Result5> sharedServices, HttpClient httpClient) : base(sharedServices)
        {
            this.httpClient = httpClient;
        }

        protected override Html4 ParseUnit(QueryString3 model)
        {
            var body = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("svc", "1"),
                new KeyValuePair<string, string>("q", model.QueryString)
            });

            var result = httpClient.PostAsync("https://911auto.com.ua", body).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var html = result.Content.ReadAsStringAsync().Result;
                return new Html4 { Html = html };
            }
            throw new Exception("Get Request Status is Not 200OK");
        }
    }
}
using ParserApi.Parsers.Site911Demo.Models;
using ParserCoreProject.Abstraction;
using System;
using System.Net;
using System.Net.Http;

namespace ParserApi.Parsers.Site911Demo.WorkUnits
{
    public class Step1 : WorkerBase<In1, Html2, In1, Html2, Result5>
    {
        private readonly HttpClient httpClient;

        public Step1(IWorkerSharedServices<Result5> sharedServices, HttpClient httpClient) : base(sharedServices)
        {
            this.httpClient = httpClient;
        }

        protected override Html2 ParseUnit(In1 model)
        {
            var uri = $"http://911auto.com.ua/search/{model.Id}";
            var result = httpClient.GetAsync(uri).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var html = result.Content.ReadAsStringAsync().Result;
                return new Html2 { Html = html };
            }
            throw new Exception("Get Request Status is Not 200OK"); // return some code ? | depend on mode - detailed exception or not
        }
    }
}
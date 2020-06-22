//using ParserApi.Parsers.Site911EventBusPrototype.Models;
//using SiteParsingHelper.Event.Abstraction;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;

//namespace ParserApi.Parsers.Site911EventBusPrototype.WorkUnits
//{
//    public class C_RequestPartDetails : WorkUnitBase<B_ParseHtmlOutput, C_QueryResultOutput, Site911ParsingResult>
//    {
//        private readonly HttpClient httpClient;

//        public C_RequestPartDetails(IWebParser<Site911ParsingResult> eventBus, HttpClient httpClient) : base(eventBus)
//        {
//            this.httpClient = httpClient;
//        }

//        protected override C_QueryResultOutput ParseUnit(B_ParseHtmlOutput model)
//        {
//            var body = new FormUrlEncodedContent(new[]{
//                new KeyValuePair<string, string>("svc", "1"),
//                new KeyValuePair<string, string>("q", model.QueryString)
//            });

//            var result = httpClient.PostAsync("https://911auto.com.ua", body).Result;

//            if (result.StatusCode == HttpStatusCode.OK)
//            {
//                var html = result.Content.ReadAsStringAsync().Result;
//                return new C_QueryResultOutput { Html = html };
//            }
//            throw new Exception("Get Request Status is Not 200OK");
//        }

//        protected override void SelectWorkUnit(C_QueryResultOutput model)
//        {
//            webParser.ExecuteUnit<C_QueryResultOutput, Site911ParsingResult>(model);
//        }
//    }
//}
using ParserApi.Parsers.Site911EventBusPrototype.Models;
using System;
using System.Net;
using System.Net.Http;

namespace ParserApi.Parsers.Site911EventBusPrototype.WorkUnits
{
    // Seems it is ok to limit searchable models, currently can register everything...
    //public class A_SearchModelByIdRequest : WorkUnitBase<A_SearchModelByIdRequestInput, A_SearchModelByIdRequestOutput, Site911ParsingResult>
    //{
    //    private readonly HttpClient httpClient;

    //    public A_SearchModelByIdRequest(IWebParser<Site911ParsingResult> eventBus, HttpClient httpClient) : base(eventBus)
    //    {
    //        this.httpClient = httpClient;
    //    }

    //    protected override A_SearchModelByIdRequestOutput ParseUnit(A_SearchModelByIdRequestInput model)
    //    {
    //        var uri = $"http://911auto.com.ua/search/{model.Id}";
    //        var result = httpClient.GetAsync(uri).Result;

    //        if (result.StatusCode == HttpStatusCode.OK)
    //        {
    //            var html = result.Content.ReadAsStringAsync().Result;
    //            return new A_SearchModelByIdRequestOutput { Html = html };
    //        }
    //        throw new Exception("Get Request Status is Not 200OK"); // return some code ? | depend on mode - detailed exception or not
    //    }

    //    protected override void SelectWorkUnit(A_SearchModelByIdRequestOutput model)
    //    {
    //        webParser.ExecuteUnit<A_SearchModelByIdRequestOutput, B_ParseHtmlOutput>(model);
    //    }
    //}
}
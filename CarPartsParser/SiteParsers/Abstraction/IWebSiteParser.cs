namespace CarPartsParser.SiteParsers.Abstraction
{
    // it is inside as well list of steps - with input & output parameters
    public interface IWebSiteParser
    {
        IParsedResult Parse(string id);
    }

    // sp.Add(BaseParcingStep step) - HttpRequest, JsonParsing, HtmlParsing
    // sp.Add(HttpRequest) -> DataForHtmlParsing
    // sp.Add(HtmlParse) -> DataForNewHttpRequest
    // sp.Add(JsonParse) -> FinalData
    
    // try to execute each step in case of exception - break & store exception to output object of IParsedResult
    
    // v1 - it should build chain based on input type => so till types exists it will try to execute
    // v2 - it just depend on registration sequence + input output type => more obvious
}

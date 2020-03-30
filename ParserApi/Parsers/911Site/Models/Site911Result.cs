using SiteParsingHelper;

namespace ParserApi.Parsers.Site911Parser
{
    public class Site911Result : ParserExecutorResultBase
    {
        public string ModelName { get; set; }
        public decimal Price { get; set; }
    }
}
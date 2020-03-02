using CarPartsParser.SiteParsers.Abstraction;

namespace CarPartsParser.SiteParsers.SiteB
{
    public class WebSiteParserB : IWebSiteParser
    {
        public IParsedResult Parse(string id)
        {
            return new ResultB();
        }
    }
}

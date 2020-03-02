using CarPartsParser.SiteParsers.Abstraction;
using System;

namespace CarPartsParser.SiteParsers.SiteA
{
    public class WebSiteParserA : IWebSiteParser
    {
        public IParsedResult Parse(string id)
        {
            return new ResultA();
        }
    }
}

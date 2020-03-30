using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using SiteParsingHelper;
using SiteParsingHelper.Tree;

namespace ParserApi.Parsers.Site911Parser
{
    public class Site911Parser : WebSiteParser<ISite911WorkUnit, Site911Result>
    {
        public Site911Parser(WorkUnitTree tree) : base(tree)
        {
        }
    }
}
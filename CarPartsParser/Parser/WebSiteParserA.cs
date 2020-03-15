using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Parser.Tree;

namespace CarPartsParser.Parser
{
    public class WebSiteParserA : WebSiteParser<IWorkUnitA>
    {
        public WebSiteParserA(WorkUnitTree tree) : base(tree)
        {
        }
    }
}

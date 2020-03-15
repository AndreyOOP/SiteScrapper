using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Parser.Tree;
using CarPartsParser.SiteParsers.Abstraction;

namespace CarPartsParser.Parser
{
    public class WebSiteParserB : WebSiteParser<IWorkUnitB>
    {
        public WebSiteParserB(WorkUnitTree tree) : base(tree)
        {
        }
    }
}

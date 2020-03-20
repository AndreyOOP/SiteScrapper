using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Parser.Tree;
using CarPartsParser.Models;

namespace CarPartsParser.Parser
{
    public class WebSiteParserA : WebSiteParser<IWorkUnitA, ParserExecutorResult>
    {
        public WebSiteParserA(WorkUnitTree tree) : base(tree)
        {
        }
    }
}

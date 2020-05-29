using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Models;
using CarPartsParser.Parser.Tree;
using CarPartsParser.SiteParsers.Abstraction;

namespace CarPartsParser.Parser
{
    public class WebSiteParserB : WebSiteParser<IWorkUnitB, ParserExecutorResultBase>
    {
        public WebSiteParserB(WorkUnitTree tree) : base(tree)
        {
        }
    }
}

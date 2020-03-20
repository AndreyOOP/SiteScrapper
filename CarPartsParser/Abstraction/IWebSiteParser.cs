using CarPartsParser.Abstraction.Models;
using CarPartsParser.Models;

namespace CarPartsParser.SiteParsers.Abstraction
{
    public interface IWebSiteParser<out TOut> where TOut : ParserExecutorResultBase, new()
    {
        TOut Parse(IWorkUnitModel input);
    }
}

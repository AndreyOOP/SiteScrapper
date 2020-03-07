using CarPartsParser.Abstraction.Models;

namespace CarPartsParser.SiteParsers.Abstraction
{
    public interface IWebSiteParser
    {
        IWorkUnitModel Parse(IWorkUnitModel input);
    }
}

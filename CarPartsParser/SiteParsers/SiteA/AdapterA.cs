using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.SiteParsers.RootParser;

namespace CarPartsParser.SiteParsers.SiteA
{
    public class AdapterA : IResultAdapter<ResultA>
    {
        public RootResult Convert(IParsedResult res)
        {
            return new RootResult();
        }
    }
}

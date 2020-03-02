using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.SiteParsers.RootParser;

namespace CarPartsParser.SiteParsers.SiteB
{
    public class AdapterB : IResultAdapter<ResultB>
    {
        public RootResult Convert(IParsedResult res)
        {
            return new RootResult();
        }
    }
}

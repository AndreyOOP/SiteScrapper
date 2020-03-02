using CarPartsParser.SiteParsers.RootParser;

namespace CarPartsParser.SiteParsers.Abstraction
{
    public interface IResultAdapter<out T> where T : IParsedResult
    {
        RootResult Convert(IParsedResult res);
    }
}

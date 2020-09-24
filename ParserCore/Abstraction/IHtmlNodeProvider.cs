using HtmlAgilityPack;

namespace ParserCore.Abstraction
{
    public interface IHtmlNodeProvider
    {
        HtmlNode Node { get; }
    }
}

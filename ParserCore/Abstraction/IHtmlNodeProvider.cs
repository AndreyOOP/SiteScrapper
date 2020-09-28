using HtmlAgilityPack;

namespace ParserCore.Abstraction
{
    public interface IHtmlNodeProvider
    {
        /// <summary>
        /// Provides html node. Depend on implementation it could be received in different ways - from another node, parsed from html etc
        /// </summary>
        HtmlNode Node { get; }
    }
}

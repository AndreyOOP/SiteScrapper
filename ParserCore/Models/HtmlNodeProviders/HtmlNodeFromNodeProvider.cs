using HtmlAgilityPack;
using ParserCore.Abstraction;

namespace ParserCore.Models.HtmlNodeProviders
{
    /// <summary>
    /// Provides html node used during creation
    /// </summary>
    public class HtmlNodeFromNodeProvider : IHtmlNodeProvider
    {
        public HtmlNode Node { get; private set; }

        public HtmlNodeFromNodeProvider(HtmlNode htmlNode)
        {
            Node = htmlNode;
        }
    }
}

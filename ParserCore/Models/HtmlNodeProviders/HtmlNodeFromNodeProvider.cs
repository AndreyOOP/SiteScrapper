using HtmlAgilityPack;
using ParserCore.Abstraction;

namespace ParserCore.Models.HtmlNodeProviders
{
    public class HtmlNodeFromNodeProvider : IHtmlNodeProvider
    {
        public HtmlNode Node { get; private set; }

        public HtmlNodeFromNodeProvider(HtmlNode htmlNode)
        {
            Node = htmlNode;
        }
    }
}

using HtmlAgilityPack;
using ParserCore.Abstraction;

namespace ParserCore.Models.HtmlNodeProviders
{
    public class HtmlNodeFromHtmlProvider : IHtmlNodeProvider
    {
        private string html;
        private HtmlNode htmlNode;

        public HtmlNode Node {
            get
            {
                if(htmlNode == null)
                {
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);
                    htmlNode = htmlDocument.DocumentNode;
                }
                return htmlNode;
            }
        }

        public HtmlNodeFromHtmlProvider(string html)
        {
            this.html = html;
        }
    }
}

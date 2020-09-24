using HtmlAgilityPack;
using ParserCore.Abstraction;

namespace ParserCore.Models.HtmlNodeProviders
{
    /// <summary>
    /// Provides html node created based on html
    /// </summary>
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

        /// <param name="html">Html source of the html node</param>
        public HtmlNodeFromHtmlProvider(string html)
        {
            this.html = html;
        }
    }
}

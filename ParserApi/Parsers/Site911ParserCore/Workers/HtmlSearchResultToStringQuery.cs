using HtmlAgilityPack;
using ParserApi.Parsers.Site911ParserCore.Models;

namespace ParserApi.Parsers.Site911ParserCore.Workers
{
    public class HtmlSearchResultToStringQuery : WorkerBase<HtmlSearchResult2, StringQuery3>
    {
        private HtmlDocument htmlDocument;

        public HtmlSearchResultToStringQuery(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public override StringQuery3 Parse(HtmlSearchResult2 model)
        {
            htmlDocument.LoadHtml(model.Html);

            var script = htmlDocument.DocumentNode.SelectSingleNode("//script//text()[contains(., 'zakaz_blk_svc')]");

            return new StringQuery3
            {
                QueryString = GetQuery(script.InnerText)
            };
        }

        private string GetQuery(string script) // add text parser
        {
            var from = script.IndexOf("svc=1&q=");
            var firstPart = script.Substring(from);
            var to = firstPart.IndexOf("'");

            return firstPart.Substring(0, to);
        }
    }
}
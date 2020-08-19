using HtmlAgilityPack;
using ParserApi.Parsers.Site911ParserCore.Models;

namespace ParserApi.Parsers.Site911ParserCore.Workers
{
    public class HtmlSearchResultToOutModelName : WorkerBase<HtmlSearchResult2, OutModelName3>
    {
        private HtmlDocument htmlDocument;

        public HtmlSearchResultToOutModelName(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public override OutModelName3 Parse(HtmlSearchResult2 model)
        {
            htmlDocument.LoadHtml(model.Html);

            var productHeader = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='product_list']");
            var modelName = productHeader.SelectSingleNode("//table/tr[@class='hl']/td/a")?.InnerText; // can be missing

            return new OutModelName3
            {
                ModelName = modelName
            };
        }
    }
}
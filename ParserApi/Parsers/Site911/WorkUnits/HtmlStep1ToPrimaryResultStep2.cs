using HtmlAgilityPack;
using ParserApi.Exceptions;
using ParserApi.Extensions;
using ParserApi.Parsers.Site911.Models;
using System.Linq;

namespace ParserApi.Parsers.Site911.WorkUnits
{
    public class HtmlStep1ToPrimaryResultStep2 : Site911WorkerBase<HtmlStep1, PrimaryResultStep2>
    {
        public override bool IsExecutable(HtmlStep1 model) 
            => LoadProductTable(model.Html) != null;

        public override PrimaryResultStep2 Parse(HtmlStep1 model)
        {
            var productTable = LoadProductTable(model.Html);

            var nodes = productTable?.SelectSingleNode("//tr[@class='hl']")?.ChildNodes?.ToArray();

            if (nodes == null || nodes.Count() < 7)
                throw new ParsingException("Unexpected product table structure");

            return new PrimaryResultStep2
            {
                Name = nodes[0].InnerText,
                InStockDnepr = nodes[2].GetSign(),
                InStockKiev = nodes[3].GetSign(),
                KievOneDay = nodes[5].GetSign(),
                Price = double.Parse(nodes[6].InnerText)
            };
        }

        private HtmlNode LoadProductTable(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument.DocumentNode.SelectSingleNode("//table[@id='product_tbl']");
        }
    }
}
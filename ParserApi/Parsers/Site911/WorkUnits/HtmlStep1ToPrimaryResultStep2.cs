using HtmlAgilityPack;
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

            var nodes = productTable.SelectSingleNode("//tr[@class='hl']").ChildNodes.ToArray();

            return new PrimaryResultStep2
            {
                Name = nodes[0].InnerText,
                InStockDnepr = nodes[2].FirstChild.Attributes["src"].Value.EndsWith("plus.gif"),
                InStockKiev = nodes[3].FirstChild.Attributes["src"].Value.EndsWith("plus.gif"),
                KievOneDay = nodes[5].FirstChild.Attributes["src"].Value.EndsWith("plus.gif"),
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
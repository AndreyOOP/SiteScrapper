using HtmlAgilityPack;
using ParserApi.Parsers.Site911ParserCore.Models;
using System.Linq;

namespace ParserApi.Parsers.Site911ParserCore.Workers
{
    public class HtmlQueryResultToOutTable : WorkerBase<HtmlQueryResult4, OutTable5>
    {
        private HtmlDocument htmlDocument;

        public HtmlQueryResultToOutTable(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public override OutTable5 Parse(HtmlQueryResult4 model)
        {
            htmlDocument.LoadHtml(model.Html);

            int i = -1;
            var table = htmlDocument.DocumentNode.SelectNodes("//table/tr")
                                    .Skip(1)
                                    .Select(n => new TableRow
                                    {
                                        Price = n.SelectSingleNode($"//td[@id='zpuah{++i}']").InnerText,
                                        Days = n.SelectSingleNode($"//td[@id='zdep{i}']").InnerText
                                    }).ToList();

            return new OutTable5
            {
                Table = table
            };
        }
    }
}
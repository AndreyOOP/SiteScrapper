using HtmlAgilityPack;
using ParserApi.Exceptions;
using ParserApi.Parsers.Site911.Models;
using System.Linq;

namespace ParserApi.Parsers.Site911.WorkUnits
{
    public class HtmlStep3ToSecondaryResultStep4 : Site911WorkerBase<HtmlStep3, SecondaryResultStep4>
    {
        public override SecondaryResultStep4 Parse(HtmlStep3 model)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(model.Html);

            var table = htmlDocument.DocumentNode.SelectNodes("//table/tr")?.Skip(1)
                                    .Select(node =>
                                    {
                                        var childNodes = node?.ChildNodes?.ToArray();

                                        if (childNodes ==null || childNodes.Count() < 7)
                                            throw new ParsingException("Unexpected secondary table structure");

                                        return new SecondaryResultTableRow
                                        {
                                            PartId = childNodes[0].InnerText,
                                            Brand = childNodes[1].InnerText,
                                            Name = childNodes[2].InnerText,
                                            Qty = int.Parse(childNodes[3].InnerText),
                                            Storage = childNodes[4].InnerText,
                                            DeliveryDays = childNodes[5].InnerText,
                                            PriceUah = double.Parse(childNodes[6].InnerText)
                                        };
                                    }).ToArray();

            return new SecondaryResultStep4
            {
                Table = table
            };
        }
    }
}
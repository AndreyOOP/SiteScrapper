using HtmlAgilityPack;
using ParserApi.Parsers.Autoklad.Models;
using System.Linq;

namespace ParserApi.Parsers.Autoklad.WorkUnits
{
    public class HtmlToFirstResultAK : AutokladWorkerBase<HtmlS1AK, FirstResultAK>
    {
        public override FirstResultAK Parse(HtmlS1AK model)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(model.Html);

            var searchResult = htmlDocument.DocumentNode.SelectSingleNode("//ul[@class='uk-nav uk-nav-autocomplete uk-autocomplete-results']");

            if (searchResult == null)
                return new FirstResultAK();

            return new FirstResultAK
            {
                FirstResult = searchResult.SelectNodes("li").Select(r => new FirstResultAKTableRow
                {
                    PartBrandLink = r.SelectSingleNode("a[@href]")?.Attributes["href"]?.Value,
                    Brand = r.SelectSingleNode("a//span[@class='o-autocomplete-brand']")?.InnerText,
                    Name = r.SelectSingleNode("a//span[@class='o-autocomplete-name']")?.InnerText,
                    Model = r.SelectSingleNode("a//span[@class='o-autocomplete-model']")?.InnerText,
                    Price = r.SelectSingleNode("a//span[@class='o-autocomplete-price']")?.InnerText
                }).ToList()
            };
        }
    }
}
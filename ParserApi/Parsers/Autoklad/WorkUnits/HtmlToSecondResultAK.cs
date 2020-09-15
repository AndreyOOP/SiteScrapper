using HtmlAgilityPack;
using ParserApi.Parsers.Autoklad.Models;
using System.Collections.Generic;
using System.Linq;

namespace ParserApi.Parsers.Autoklad.WorkUnits
{
    public class HtmlToSecondResultAK : AutokladWorkerBase<HtmlsS3, SecondResultAK>
    {
        public override bool IsExecutable(HtmlsS3 model)
            => model.Htmls.Count > 0;

        public override SecondResultAK Parse(HtmlsS3 model)
        {
            var result = new SecondResultAK { 
                Analogs = new Dictionary<string, List<RowResultAK>>(),
                Same = new Dictionary<string, List<RowResultAK>>()
            };

            foreach(var html in model.Htmls)
            {
                result.Analogs[html.Key] = ParseTable("aanalog", html.Value);
                result.Same[html.Key] = ParseTable("asovpad", html.Value);
            }

            return result;
        }

        private List<RowResultAK> ParseTable(string tableName, string html)
        {
            var result = new List<RowResultAK>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var table = htmlDocument.DocumentNode.SelectSingleNode($"//h5[@id='{tableName}']");
            if (table == null)
                return result;

            var trNodes = table?.ParentNode?.ParentNode?.SelectNodes(".//div[@id='table_div']//tr[@id='tr']");
            result = trNodes.Select(tr => new RowResultAK
            {
                Name = tr.SelectSingleNode(".//td[1]/a").InnerText,
                Period = tr.SelectSingleNode(".//td[2]").InnerText,
                Price = tr.SelectSingleNode(".//td[3]").InnerText,
            }).ToList();
            return result;
        }
    }
}
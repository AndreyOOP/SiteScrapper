using HtmlAgilityPack;
using ParserApi.Parsers._911Site.Models;
using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using SiteParsingHelper;
using SiteParsingHelper.Abstraction;
using System.Linq;

namespace ParserApi.Parsers._911Site.WorkUnits
{
    public class D0parseHtmlPartDetails : ISite911WorkUnit
    {
        public IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            var htmlDoc = new HtmlDocument();
            var c0httpResponse = (C0httpResponse)input;

            htmlDoc.LoadHtml(c0httpResponse.Html);

            int i = -1;
            var table = htmlDoc.DocumentNode.SelectNodes("//table/tr")
                               .Skip(1)
                               .Select(n => new TableRow { 
                                   Price = n.SelectSingleNode($"//td[@id='zpuah{++i}']").InnerText,
                                   Days = n.SelectSingleNode($"//td[@id='zdep{i}']").InnerText
                               }).ToList();

            var x = (Site911Result)siteParserResult;
            x.Table = table;

            return null;
        }
    }
}
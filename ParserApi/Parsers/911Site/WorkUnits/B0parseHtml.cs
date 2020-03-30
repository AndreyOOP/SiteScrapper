using HtmlAgilityPack;
using ParserApi.Parsers._911Site.Models;
using ParserApi.Parsers.Site911Parser;
using ParserApi.Parsers.Site911Parser.Models;
using ParserApi.Parsers.Site911Parser.WorkUnits;
using SiteParsingHelper;
using SiteParsingHelper.Abstraction;

namespace ParserApi.Parsers.Site911.WorkUnits
{
    public class B0parseHtml : ISite911WorkUnit
    {
        public IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            var htmlDoc = new HtmlDocument(); // use DI to inject new instance
            var a0httpResponse = (A0httpResponse)input;

            htmlDoc.LoadHtml(a0httpResponse.Html);
            
            var productHeader = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='product_list']");
            var name = productHeader.SelectSingleNode("//table/tr[@class='hl']/td/a").InnerText;
            var site911Result = (Site911Result)siteParserResult; 
            site911Result.ModelName = name;

            var script = htmlDoc.DocumentNode.SelectSingleNode("//script[@type='text/javascript']//text()[contains(., 'zakaz_blk_svc')]");

            return new B0parseHtmlResult
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
using HtmlAgilityPack;
using ParserApi.Parsers.Site911EventBusPrototype.Models;
using SiteParsingHelper.Event.Abstraction;
using System.Linq;

namespace ParserApi.Parsers.Site911EventBusPrototype.WorkUnits
{
    public class D_ParseHtmlPartDetails : WorkUnitBase<C_QueryResultOutput, Site911ParsingResult, Site911ParsingResult>
    {
        private HtmlDocument htmlDocument;

        public D_ParseHtmlPartDetails(IWebParser<Site911ParsingResult> eventBus, HtmlDocument htmlDocument) : base(eventBus)
        {
            this.htmlDocument = htmlDocument;
        }

        protected override Site911ParsingResult ParseUnit(C_QueryResultOutput model)
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

            webParser.Result.Table = table;

            return new Site911ParsingResult(); // ToDo: avoid this
        }

        protected override void SelectWorkUnit(Site911ParsingResult model)
        {
            return; // as well not obvious if it is last unit & how to call it - should be sms like webParser.Stop() / webParser.NotCallNext(); webParser.CallUnitWithSignature<>(); webParser.CallNextDefault<TNextOutModel>() - maybe do as default implementation
        }
    }
}
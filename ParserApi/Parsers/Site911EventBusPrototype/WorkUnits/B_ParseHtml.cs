//using HtmlAgilityPack;
//using ParserApi.Parsers.Site911EventBusPrototype.Models;
//using SiteParsingHelper.Event.Abstraction;

//namespace ParserApi.Parsers.Site911EventBusPrototype.WorkUnits
//{
//    public class B_ParseHtml : WorkUnitBase<A_SearchModelByIdRequestOutput, B_ParseHtmlOutput, Site911ParsingResult>
//    {
//        private HtmlDocument htmlDocument;

//        public B_ParseHtml(IWebParser<Site911ParsingResult> eventBus, HtmlDocument htmlDocument) : base(eventBus)
//        {
//            this.htmlDocument = htmlDocument;
//        }

//        protected override B_ParseHtmlOutput ParseUnit(A_SearchModelByIdRequestOutput model)
//        {
//            htmlDocument.LoadHtml(model.Html);

//            var productHeader = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='product_list']");
//            var modelName = productHeader.SelectSingleNode("//table/tr[@class='hl']/td/a")?.InnerText; // can be missing

//            webParser.Result.ModelName = modelName;

//            //var script = htmlDocument.DocumentNode.SelectSingleNode("//script[@type='text/javascript']//text()[contains(., 'zakaz_blk_svc')]");
//            var script = htmlDocument.DocumentNode.SelectSingleNode("//script//text()[contains(., 'zakaz_blk_svc')]");

//            return new B_ParseHtmlOutput
//            {
//                QueryString = GetQuery(script.InnerText)
//            };
//        }

//        protected override void SelectWorkUnit(B_ParseHtmlOutput model)
//        {
//            webParser.ExecuteUnit<B_ParseHtmlOutput, C_QueryResultOutput>(model);
//        }

//        private string GetQuery(string script) // add text parser
//        {
//            var from = script.IndexOf("svc=1&q=");
//            var firstPart = script.Substring(from);
//            var to = firstPart.IndexOf("'");

//            return firstPart.Substring(0, to);
//        }
//    }
//}
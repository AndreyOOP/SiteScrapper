using HtmlAgilityPack;
using ParserApi.Parsers.Site911.Models;

namespace ParserApi.Parsers.Site911.WorkUnits
{
    public class HtmlStep1ToQueryStringStep2 : Site911WorkerBase<HtmlStep1, QueryStringStep2>
    {
        public override bool IsExecutable(HtmlStep1 model)
            => GetScript(model.Html) != null;

        public override QueryStringStep2 Parse(HtmlStep1 model)
        {
            var script = GetScript(model.Html).InnerText;
            var from = script.IndexOf("svc=1&q=");
            var to = script.IndexOf("');");
            return new QueryStringStep2(script.Substring(from, to-from));
        }

        private HtmlNode GetScript(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument.DocumentNode.SelectSingleNode("//script//text()[contains(., 'zakaz_blk_svc')]");
        }
    }
}
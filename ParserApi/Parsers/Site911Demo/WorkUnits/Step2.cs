using HtmlAgilityPack;
using ParserApi.Parsers.Site911Demo.Models;

namespace ParserApi.Parsers.Site911Demo.WorkUnits
{
    //public class Step2 : WorkerBase<Html2, QueryString3, In1, Html2, Result5>
    //{
    //    private HtmlDocument htmlDocument;

    //    public Step2(IWorkerSharedServices<Result5> sharedServices, HtmlDocument htmlDocument) : base(sharedServices)
    //    {
    //        this.htmlDocument = htmlDocument;
    //    }

    //    protected override QueryString3 ParseUnit(Html2 model)
    //    {
    //        htmlDocument.LoadHtml(model.Html);

    //        var productHeader = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='product_list']");
    //        var modelName = productHeader.SelectSingleNode("//table/tr[@class='hl']/td/a")?.InnerText; // can be missing

    //        sharedServices.Result.ModelName = modelName;

    //        //var script = htmlDocument.DocumentNode.SelectSingleNode("//script[@type='text/javascript']//text()[contains(., 'zakaz_blk_svc')]");
    //        var script = htmlDocument.DocumentNode.SelectSingleNode("//script//text()[contains(., 'zakaz_blk_svc')]");

    //        return new QueryString3
    //        {
    //            QueryString = GetQuery(script.InnerText)
    //        };
    //    }

    //    private string GetQuery(string script) // add text parser
    //    {
    //        var from = script.IndexOf("svc=1&q=");
    //        var firstPart = script.Substring(from);
    //        var to = firstPart.IndexOf("'");

    //        return firstPart.Substring(0, to);
    //    }
    //}
}
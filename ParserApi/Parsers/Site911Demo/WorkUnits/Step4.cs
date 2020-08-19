using HtmlAgilityPack;
using ParserApi.Parsers.Site911Demo.Models;
using System.Linq;

namespace ParserApi.Parsers.Site911Demo.WorkUnits
{
    //public class Step4 : WorkerBase<Html4, Result5, In1, Html2, Result5>
    //{
    //    private HtmlDocument htmlDocument;

    //    public Step4(IWorkerSharedServices<Result5> sharedServices, HtmlDocument htmlDocument) : base(sharedServices)
    //    {
    //        this.htmlDocument = htmlDocument;
    //    }

    //    protected override bool StopHere => true;

    //    protected override Result5 ParseUnit(Html4 model)
    //    {
    //        htmlDocument.LoadHtml(model.Html);

    //        int i = -1;
    //        var table = htmlDocument.DocumentNode.SelectNodes("//table/tr")
    //                                .Skip(1)
    //                                .Select(n => new TableRow
    //                                {
    //                                    Price = n.SelectSingleNode($"//td[@id='zpuah{++i}']").InnerText,
    //                                    Days = n.SelectSingleNode($"//td[@id='zdep{i}']").InnerText
    //                                }).ToList();

    //        sharedServices.Result.Table = table;

    //        return sharedServices.Result; // it is possible to return anything
    //    }
    //}
}
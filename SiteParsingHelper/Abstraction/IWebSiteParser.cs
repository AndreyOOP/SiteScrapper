namespace SiteParsingHelper.Abstraction
{
    public interface IWebSiteParser<out TOut> where TOut : ParserExecutorResultBase, new()
    {
        TOut Parse(IWorkUnitModel input);
    }
}

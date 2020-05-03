namespace SiteParsingHelper.Event.Abstraction
{
    public interface IUnit<TIn>
    {
        void ParseAndSelectNext(TIn model);
    }
}

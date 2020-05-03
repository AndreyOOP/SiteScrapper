namespace SiteParsingHelper.Event.Abstraction
{
    public interface IWebParser<TResult> 
    {
        /// <summary>
        /// Final parsing result. It could be updated on any step (any WorkUnit has access to it)
        /// </summary>
        TResult Result { get; }

        /// <summary>
        /// Register WorkUnit which create TOut model from TIn
        /// TIn and TOut required for type safety - e.g. workUnit inherited from <see cref="WorkUnitBase{TIn, TOut, TParserResult}"/>
        /// </summary>
        void RegisterUnit<TIn, TOut>(IUnit<TIn> workUnit);

        /// <summary>
        /// Select WorkUnit based on TIn, TOut types combination. WorkUnit create TOut model from TIn and call next WorkUnit for execution
        /// </summary>
        void ExecuteUnit<TIn, TOut>(TIn model);
    }
}
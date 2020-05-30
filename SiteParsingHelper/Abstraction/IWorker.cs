namespace ParserCoreProject.Abstraction
{
    public interface IWorker<TIn, TOut>
    {
        void ParseAndExecuteNext(TIn model);
    }
}

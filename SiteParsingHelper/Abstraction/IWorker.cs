namespace ParserCoreProject.Abstraction
{
    public interface IWorker<TIn, TOut>
    {
        void ParseAndExecuteNext(TIn model);
    }

    public interface IWorker2<TIn, TOut>
    {
        TOut Parse(TIn model);
    }

    public interface IWorker3
    {
        object Parse<TIn>(TIn model);
    }
}

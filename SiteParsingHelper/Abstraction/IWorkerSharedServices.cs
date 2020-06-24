namespace ParserCoreProject.Abstraction
{
    public interface IWorkerSharedServices<TFirstIn, TFirstOut, TResult>
    {
        IWorkersContainer<TFirstIn, TFirstOut> WorkersContainer { get; }

        IWorkerPreprocessorsContainer WorkersPreprocessorsContainer { get; }

        TResult Result { get; }
    }
}

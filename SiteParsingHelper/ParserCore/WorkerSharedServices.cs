using ParserCoreProject.Abstraction;

namespace ParserCoreProject.ParserCore
{
    public class WorkerSharedServices<TFirstIn, TFirstOut, TResult> : IWorkerSharedServices<TFirstIn, TFirstOut, TResult>
    {
        public IWorkersContainer<TFirstIn, TFirstOut> WorkersContainer { get; }

        public IWorkerPreprocessorsContainer WorkersPreprocessorsContainer { get; }

        public TResult Result { get; }

        public WorkerSharedServices(IWorkersContainer<TFirstIn, TFirstOut> workersContainer, IWorkerPreprocessorsContainer workersPreprocessorsContainer, TResult result)
        {
            WorkersContainer = workersContainer;
            WorkersPreprocessorsContainer = workersPreprocessorsContainer;
            Result = result;
        }
    }
}

using ParserCoreProject.Abstraction;
using System;

namespace ParserCoreProject.ParserCore
{
    public class WorkerSharedServices<TFirstIn, TFirstOut, TResult> : IWorkerSharedServices<TFirstIn, TFirstOut, TResult>
    {
        public IWorkersContainer<TFirstIn, TFirstOut> WorkersContainer { get; }

        public IWorkerPreprocessorsContainer WorkersPreprocessorsContainer { get; }

        public TResult Result { get; }

        public Exception ExceptionInParseAndExecuteNext { get; set; }

        public WorkerSharedServices(IWorkersContainer<TFirstIn, TFirstOut> workersContainer, IWorkerPreprocessorsContainer workersPreprocessorsContainer, TResult result)
        {
            WorkersContainer = workersContainer;
            WorkersPreprocessorsContainer = workersPreprocessorsContainer;
            WorkersPreprocessorsContainer.RegisterPreprocessor(new ExecutionPathPreprocessor(new ExecutionPath())); // Temp solution
            Result = result;
        }
    }
}

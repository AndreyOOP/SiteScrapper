using ParserCoreProject.Abstraction;
using System;

namespace ParserCoreProject.ParserCore
{
    // ToDo: Remove
    public class WorkerSharedServices<TResult> : IWorkerSharedServices<TResult>
    {
        public IWorkersContainer WorkersContainer { get; }

        public IWorkerPreprocessorsContainer WorkersPreprocessorsContainer { get; }

        public TResult Result { get; }

        public Exception ExceptionInParseAndExecuteNext { get; set; }

        public WorkerSharedServices(IWorkersContainer workersContainer, IWorkerPreprocessorsContainer workersPreprocessorsContainer, TResult result)
        {
            WorkersContainer = workersContainer;
            WorkersPreprocessorsContainer = workersPreprocessorsContainer;
            WorkersPreprocessorsContainer.RegisterPreprocessor(new ExecutionPathPreprocessor(new ExecutionPath())); // Temp solution
            Result = result;
        }
    }
}

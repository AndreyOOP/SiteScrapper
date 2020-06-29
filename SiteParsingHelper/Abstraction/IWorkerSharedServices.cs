using System;

namespace ParserCoreProject.Abstraction
{
    public interface IWorkerSharedServices<TResult>
    {
        IWorkersContainer WorkersContainer { get; }

        IWorkerPreprocessorsContainer WorkersPreprocessorsContainer { get; }

        TResult Result { get; }

        Exception ExceptionInParseAndExecuteNext { get; set; }
    }
}

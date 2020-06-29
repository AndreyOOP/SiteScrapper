using ParserCoreProject.Exceptions;
using ParserCoreProject.ParserCore;
using System;

namespace ParserCoreProject.Abstraction
{
    /// <summary>
    /// All workers are inherited from this class
    /// It knows how to parse (convert) input model TIn to output TOut & select next worker for execution
    /// </summary>
    public abstract class WorkerBase<TIn, TOut, TFirstIn, TFirstOut, TResult> : IWorker<TIn, TOut>
    {
        protected IWorkerSharedServices<TResult> sharedServices;

        protected WorkerBase(IWorkerSharedServices<TResult> sharedServices)
        {
            this.sharedServices = sharedServices;
        }

        /// <summary>
        /// Override value to true if this Worker expected to be last one into execution chain
        /// No next worker will be selected
        /// </summary>
        protected virtual bool StopHere { get; private set; } = false;

        protected abstract TOut ParseUnit(TIn model);

        /// <summary>
        /// Default implementation - it will select next worker by TOut if it is possible
        /// If it is not possible method has to be overrided & next worker has to be explicitly called
        /// </summary>
        protected virtual void ExecuteNextWorker(TOut model)
        {
            object worker = sharedServices.WorkersContainer.GetIfSingleImplementation<TOut>();

            var methodInfo = worker.GetType().GetMethod(nameof(this.ParseAndExecuteNext));

            methodInfo.Invoke(worker, new object[] { model });
        }


        /// <exception cref="WorkUnitException">Throws in case of any exception during method execution. Contains inner exception with original exception. 
        /// Contains ExecutionPath where chain of executed modules stored</exception>
        public void ParseAndExecuteNext(TIn model)
        {
            try
            {
                foreach (var preprocessor in sharedServices.WorkersPreprocessorsContainer.GetPreprocessors())
                {
                    preprocessor.Execute(this);
                }

                TOut outResult = ParseUnit(model);

                if (StopHere == true)
                    return;

                ExecuteNextWorker(outResult);
            }
            catch (Exception ex)
            {
                var workUnitException = new WorkUnitException(string.Format(Resource.WorkUnitParseAndExecuteExecption, typeof(TIn).Name), ex);
                workUnitException.ExecutionPath = sharedServices.WorkersPreprocessorsContainer.GetPreprocessor<ExecutionPathPreprocessor>()?.ExecutionPath;

                sharedServices.ExceptionInParseAndExecuteNext = workUnitException;
            }

            // If we rethrow exception in catch block - we will have a lot of inner exceptions
            // currently exception stored to shared data & throwed from first worker
            if (typeof(TIn) == typeof(TFirstIn) && sharedServices.ExceptionInParseAndExecuteNext != null)
                throw sharedServices.ExceptionInParseAndExecuteNext;
        }
    }
}

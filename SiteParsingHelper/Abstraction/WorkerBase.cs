namespace ParserCoreProject.Abstraction
{
    /// <summary>
    /// All workers are inherited from this class
    /// It knows how to parse (convert) input model TIn to output TOut & select next worker for execution
    /// </summary>
    public abstract class WorkerBase<TIn, TOut, TFirstIn, TFirstOut> : IWorker<TIn, TOut>
    {
        protected IWorkersContainer<TFirstIn, TFirstOut> workersContainer;

        protected WorkerBase(IWorkersContainer<TFirstIn, TFirstOut> workersContainer)
        {
            this.workersContainer = workersContainer;
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
        protected virtual void ExecuteNextWorker(TOut model, bool lastOne = false)
        {
            object worker = workersContainer.GetIfSingleImplementation<TOut>();

            var methodInfo = worker.GetType().GetMethod(nameof(this.ParseAndExecuteNext));

            methodInfo.Invoke(worker, new object[] { model });
        }

        public void ParseAndExecuteNext(TIn model)
        {
            TOut outResult = ParseUnit(model);

            if (StopHere == true)
                return;

            ExecuteNextWorker(outResult);
        }
    }
}

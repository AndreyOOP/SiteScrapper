using ParserCore;

namespace ParserApi.Parsers.Site911ParserCore
{
    public abstract class WorkerBase<TIn, TOut> : IWorker<TIn, TOut>
    {
        /// <summary>
        /// By default next model has to be proceeded
        /// </summary>
        public virtual bool IsExecutable(TIn model) => true;

        public abstract TOut Parse(TIn model);
    }
}
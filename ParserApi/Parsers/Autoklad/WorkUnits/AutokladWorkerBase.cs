using ParserCore;
using System;

namespace ParserApi.Parsers.Autoklad.WorkUnits
{
    public abstract class AutokladWorkerBase<TIn, TOut> : IWorker<TIn, TOut>
    {
        protected readonly Uri AutokladBase = new Uri("https://www.autoklad.ua/");

        public virtual bool IsExecutable(TIn model) => true;

        public abstract TOut Parse(TIn model);
    }
}
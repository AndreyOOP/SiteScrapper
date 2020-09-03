using ParserCore;
using System;

namespace ParserApi.Parsers.Site911
{
    public abstract class Site911WorkerBase<TIn, TOut> : IWorker<TIn, TOut>
    {
        protected readonly Uri Site911Base = new Uri("https://911auto.com.ua/");

        public virtual bool IsExecutable(TIn model) => true;

        public abstract TOut Parse(TIn model);
    }
}
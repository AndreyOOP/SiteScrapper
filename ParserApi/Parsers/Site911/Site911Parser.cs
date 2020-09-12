using ParserApi.Parsers.Site911.Models;
using ParserCore;
using ParserCore.Abstraction;
using ParserCore.Extensions;
using Unity;

namespace ParserApi.Parsers.Site911ParserCore
{
    public class Site911Parser : ParserBase<In, Result>
    {
        public IInMemoryWorkerLogger WorkerLogger { get; }

        public Site911Parser(
            [Dependency(nameof(Site911Parser))]IWorkersContainer workersContainer, 
            [Dependency(nameof(Site911Parser))]IInMemoryWorkerLogger workerLogger
            ) : base(workersContainer)
        {
            WorkerLogger = workerLogger;
        }

        protected override Result PrepareResult()
        {
            return new Result
            {
                Primary = result.Get<PrimaryResultStep2>(),
                Secondary = result.Get<SecondaryResultStep4>()
            };
        }
    }
}
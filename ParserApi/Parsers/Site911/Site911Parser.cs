using ParserApi.Parsers.Site911.Models;
using ParserCore;
using ParserCore.Abstraction;
using ParserCore.Extensions;
using Unity;

namespace ParserApi.Parsers.Site911ParserCore
{
    public class Site911Parser : ParserBase<In911, Result911>
    {
        public IInMemoryWorkerLogger WorkerLogger { get; }

        public Site911Parser(
            [Dependency(nameof(Site911Parser))]IWorkersContainer workersContainer, 
            [Dependency(nameof(Site911Parser))]IInMemoryWorkerLogger workerLogger
            ) : base(workersContainer)
        {
            WorkerLogger = workerLogger;
        }

        protected override Result911 PrepareResult()
        {
            return new Result911
            {
                Primary = result.Get<PrimaryResultStep2>(),
                Secondary = result.Get<SecondaryResultStep4>()
            };
        }
    }
}
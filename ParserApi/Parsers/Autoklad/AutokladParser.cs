using ParserApi.Parsers.Autoklad.Models;
using ParserCore;
using ParserCore.Abstraction;
using ParserCore.Extensions;
using Unity;

namespace ParserApi.Parsers.Autoklad
{
    public class AutokladParser : ParserBase<InAK, ResultAK>
    {
        public IInMemoryWorkerLogger WorkerLogger { get; }

        public AutokladParser(
            [Dependency(nameof(Parser.Autoklad))]IWorkersContainer workersContainer,
            [Dependency(nameof(Parser.Autoklad))]IInMemoryWorkerLogger workerLogger
            ) : base(workersContainer)
        {
            WorkerLogger = workerLogger;
        }

        protected override ResultAK PrepareResult()
        {
            return new ResultAK
            {
                HtmlS1 = result.Get<FirstResultAK>()
            };
        }
    }
}
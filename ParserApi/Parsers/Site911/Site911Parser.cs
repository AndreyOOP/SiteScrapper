using ParserApi.Parsers.Site911.Models;
using ParserCore;
using ParserCore.Extensions;

namespace ParserApi.Parsers.Site911ParserCore
{
    public class Site911Parser : ParserBase<InMod, Result>
    {
        public Site911Parser(IWorkersContainer workersContainer) : base(workersContainer)
        {
        }

        protected override Result PrepareResult()
        {
            return new Result
            {
                Res = result.Get<PrimaryResultStep2>()
            };
        }
    }
}
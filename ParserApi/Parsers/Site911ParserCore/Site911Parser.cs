using ParserApi.Parsers.Site911ParserCore.Models;
using ParserCore;
using ParserCore.Extensions;

namespace ParserApi.Parsers.Site911ParserCore
{
    public class Site911Parser : ParserBase<In, Result>
    {
        public Site911Parser(IWorkersContainer workersContainer) : base(workersContainer)
        {
        }

        protected override Result PrepareResult()
        {
            return new Result
            {
                FirstModelName = result.Get<OutModelName3>().ModelName,
                Table = result.Get<OutTable5>()
            };
        }
    }
}
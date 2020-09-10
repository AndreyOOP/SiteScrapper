using ParserApi.Parsers.Autoklad.Models;
using ParserCore;
using ParserCore.Extensions;

namespace ParserApi.Parsers.Autoklad
{
    public class AutokladParser : ParserBase<InAK, ResultAK>
    {
        public AutokladParser(IWorkersContainer workersContainer) : base(workersContainer)
        {
        }

        protected override ResultAK PrepareResult()
        {
            return new ResultAK
            {
                HtmlS1 = result.Get<HtmlS1AK>()
            };
        }
    }
}
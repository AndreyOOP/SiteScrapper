using SiteParsingHelper.Abstraction;

namespace SiteParsingHelper
{
    public class ParserExecutorResultBase : IWorkUnitModel
    {
        public string Exception { get; set; }
        public string ExecutionPath { get; set; }
    }
}

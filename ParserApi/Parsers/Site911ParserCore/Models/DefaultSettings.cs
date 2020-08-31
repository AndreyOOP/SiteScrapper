using ParserCore.Abstraction;

namespace ParserApi.Parsers.Site911ParserCore.Models
{
    public class DefaultSettings : WorkerLogSettings
    {
        public override bool ShowStackTrace => true;
        public override bool ShowExceptionMessage => true;
        public override bool AddInModel => true;
        public override bool LogOnlyParseMethod => true;
    }
}
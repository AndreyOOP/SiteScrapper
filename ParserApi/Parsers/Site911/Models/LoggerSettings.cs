using ParserCore.Abstraction;

namespace ParserApi.Parsers.Site911.Models
{
    public class VerboseLoggerSettings : WorkerLogSettings
    {
        public override bool ShowStackTrace => true;
        public override bool ShowExceptionMessage => true;
        public override bool AddInModel => true;
        public override bool LogOnlyParseMethod => false;
    }

    public class VerboseParseLoggerSettings : WorkerLogSettings
    {
        public override bool ShowStackTrace => true;
        public override bool ShowExceptionMessage => true;
        public override bool AddInModel => true;
        public override bool LogOnlyParseMethod => true;
    }

    public class ExceptionLoggerSettings : WorkerLogSettings
    {
        public override bool ShowStackTrace => true;
        public override bool ShowExceptionMessage => true;
        public override bool AddInModel => false;
        public override bool LogOnlyParseMethod => false;
    }
}
using ParserApi.Controllers.Models;
using ParserApi.Parsers.Site911.Models;
using ParserCore.Abstraction;

namespace ParserApi.Services
{
    public class LoggerSettingsProvider : ILoggerSettingsProvider
    {
        public WorkerLogSettings Settings { get; private set; } = new ExceptionLoggerSettings();

        public void Update(WorkerLogSettings settings)
        {
            Settings = settings;
        }

        public void Update(InMemoryLoggerLevel loggerLevel)
        {
            switch (loggerLevel)
            {
                case InMemoryLoggerLevel.VerboseLogger:
                    Settings = new VerboseLoggerSettings();
                    break;
                case InMemoryLoggerLevel.VerboseParseLoggerSettings:
                    Settings = new VerboseParseLoggerSettings();
                    break;
                case InMemoryLoggerLevel.ExceptionLoggerSettings:
                    Settings = new ExceptionLoggerSettings();
                    break;
            }
        }
    }
}
using ParserApi.Controllers.Models;
using ParserCore.Abstraction;

namespace ParserApi.Services
{
    public interface ILoggerSettingsProvider : ISettings<WorkerLogSettings>
    {
        /// <summary>
        /// Update WorkerLogSettings based on InMemoryLoggerLevel enum
        /// </summary>
        void Update(InMemoryLoggerLevel loggerLevel);
    }
}
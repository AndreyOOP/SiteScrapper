using ParserCore;
using ParserCore.Abstraction;

namespace ParserApi.Parsers.Site911ParserCore.Models
{
    public class Result
    {
        public string FirstModelName { get; set; }
        public OutTable5 Table { get; set; }
        public ILogger<WorkerLogRecord> logger { get; set; }
    }
}
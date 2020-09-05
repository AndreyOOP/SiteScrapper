using ParserCore;
using System.Collections.Generic;

namespace ParserApi.Parsers.Site911.Models
{
    public class Result
    {
        public PrimaryResultStep2 Primary { get; set; }
        public SecondaryResultStep4 Secondary { get; set; }
        public IEnumerable<WorkerLogRecord> Log { get; set; }
    }
}
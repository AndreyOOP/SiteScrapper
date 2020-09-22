using ParserApi.Controllers.Models;
using ParserCore;
using System.Collections.Generic;

namespace ParserApi.Parsers.Site911.Models
{
    public class Result911 : OutputSummary
    {
        public RawDataSite911 RawData { get; set; }
        public IEnumerable<WorkerLogRecord> Log { get; set; }
    }

    public class RawDataSite911
    {
        public PrimaryResultStep2 Primary { get; set; }
        public SecondaryResultStep4 Secondary { get; set; }
    }
}
using ParserApi.Controllers.Models;
using ParserCore;
using System.Collections.Generic;

namespace ParserApi.Parsers.Autoklad.Models
{
    public class ResultAK : OutputSummary
    {
        public RawDataAK RawData { get; set; }
        public IEnumerable<WorkerLogRecord> Log { get; set; }
    }

    public class RawDataAK
    {
        public IEnumerable<FirstResultAKTableRow> FirstResult { get; set; }
        public Dictionary<string, List<RowResultAK>> Analogs { get; set; }
        public Dictionary<string, List<RowResultAK>> Same { get; set; }
    }
}
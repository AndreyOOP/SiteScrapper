using ParserApi.Parsers;
using ParserCore;
using System;
using System.Collections.Generic;

namespace ParserApi.Controllers.Models
{
    public class OutputSummary
    {
        public IEnumerable<Part> Parts { get; set; }
        public IEnumerable<PerRequestPart> PerRequestParts { get; set; }
        public IEnumerable<AnalogPart> AnalogParts { get; set; }
        public IEnumerable<MatchingPart> MatchingParts { get; set; }
    }

    public class OutputSummaryWithLog : OutputSummary
    {
        /// <summary>
        /// Represent all parsed data from all parsers. Required for investigation
        /// </summary>
        public string Json { get; set; }
        public IEnumerable<WorkerLogRecord> Site911Log { get; set; }
        public IEnumerable<WorkerLogRecord> AutokladLog { get; set; }
    }

    public class Part
    {
        public string ParserName { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string LinkToSource { get; set; }
    }

    public class PerRequestPart
    {
        public string ParserName { get; set; }
        public string PartId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double PriceUah { get; set; }
        public Uri LinkToSource { get; set; }
    }

    public class AnalogPart
    {
        public string ParserName { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Uri LinkToSource { get; set; }
    }

    public class MatchingPart
    {
        public string ParserName { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Uri LinkToSource { get; set; }
    }
}

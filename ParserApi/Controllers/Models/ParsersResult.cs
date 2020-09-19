using ParserApi.Parsers;
using ParserCore;
using System;
using System.Collections.Generic;

namespace ParserApi.Controllers.Models
{
    public class ParsersResult
    {
        public IEnumerable<MainPriceTableRow> Main { get; set; }
        public IEnumerable<PerRequestTableRow> PerRequest { get; set; }
        public IEnumerable<AnalogTableRow> Analogs { get; set; }
        public IEnumerable<SameTableRow> SameIds { get; set; }
        public IEnumerable<WorkerLogRecord> Site911Log { get; set; }
        public object AutokladLog { get; set; }
    }

    public class MainPriceTableRow
    {
        public string ParserName { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string LinkToSource { get; set; }
    }

    public class PerRequestTableRow
    {
        public Parser ParserName { get; set; }
        public string PartId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double PriceUah { get; set; }
        public Uri LinkToSource { get; set; }
    }

    public class AnalogTableRow
    {
        public Parser ParserName { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Uri LinkToSource { get; set; }
    }

    public class SameTableRow
    {
        public Parser ParserName { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Uri LinkToSource { get; set; }
    }
}

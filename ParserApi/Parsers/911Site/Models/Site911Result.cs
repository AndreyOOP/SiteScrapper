using SiteParsingHelper;
using System.Collections.Generic;

namespace ParserApi.Parsers.Site911Parser
{
    public class Site911Result : ParserExecutorResultBase
    {
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<TableRow> Table { get; set; }
    }

    public class TableRow
    {
        public string Price { get; set; }
        public string Days { get; set; }
    }
}
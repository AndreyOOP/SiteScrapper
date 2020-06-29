using System.Collections.Generic;

namespace ParserApi.Parsers.Site911Demo.Models
{
    public class Result5
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
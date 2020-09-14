using System.Collections.Generic;

namespace ParserApi.Parsers.Autoklad.Models
{
    public class FirstResultAK
    {
        public List<FirstResultAKTableRow> FirstResult { get; set; }
    }

    public class FirstResultAKTableRow
    {
        public string PartBrandLink { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
    }
}
using System.Collections.Generic;

namespace ParserApi.Parsers.Autoklad.Models
{
    public class SecondResultAK
    {
        public Dictionary<string, List<RowResultAK>> Analogs { get; set; }
        public Dictionary<string, List<RowResultAK>> Same { get; set; }
    }

    public class RowResultAK
    {
        public string Name { get; set; }
        public string Period { get; set; }
        public string Price { get; set; }
    }
}
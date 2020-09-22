namespace ParserApi.Parsers.Site911.Models
{
    public class PrimaryResultStep2
    {
        public string Name { get; set; }
        public bool? InStockDnepr { get; set; }
        public bool? InStockKiev { get; set; }
        public bool? KievOneDay { get; set; }
        public double Price { get; set; }
    }
}
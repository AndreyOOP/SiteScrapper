using System.Collections.Generic;

namespace ParserApi.Parsers.Site911.Models
{
    public class SecondaryResultStep4
    {
        public IEnumerable<SecondaryResultTableRow> Table { get; set; }
    }

    public class SecondaryResultTableRow
    {
        public string PartId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public string Storage { get; set; }
        public string DeliveryDays { get; set; }
        public double PriceUah { get; set; }
    }
}
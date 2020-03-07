using CarPartsParser.Abstraction.Models;

namespace CarPartsParser.Models
{
    public class ParserExecutorResult : IWorkUnitModel
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        public string Exception { get; set; }
    }
}

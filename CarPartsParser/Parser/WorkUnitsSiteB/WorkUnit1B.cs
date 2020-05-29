using CarPartsParser.Abstraction.Models;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Models;
using CarPartsParser.Models.SiteB;

namespace CarPartsParser.Parser.WorkUnitsSiteB
{
    public class WorkUnit1B : IWorkUnitB
    {
        public IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            var inputModel = (In)input;
            siteParserResult.Prop1 = "WorkUnit_1_B set Prop1";
            return new SiteBModelB();
        }
    }
}

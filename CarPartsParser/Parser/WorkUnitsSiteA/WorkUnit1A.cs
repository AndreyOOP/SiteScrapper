using CarPartsParser.Abstraction.Models;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Models;
using CarPartsParser.Models.SiteA;

namespace CarPartsParser.SiteParsers.SiteA
{
    public class WorkUnit1A : IWorkUnitA
    {
        public IWorkUnitModel Execute(IWorkUnitModel input, ref IWorkUnitModel siteParserResult)
        {
            ((ParserExecutorResult)siteParserResult).Prop1 = "WorkUnit_1_A set Prop1";

            return new SiteAModelA();
        }
    }
}

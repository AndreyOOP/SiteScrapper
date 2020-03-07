using CarPartsParser.Abstraction.Models;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Models;
using CarPartsParser.Models.SiteA;

namespace CarPartsParser.SiteParsers.SiteA
{
    public class WorkUnit2A : IWorkUnitA
    {
        public IWorkUnitModel Execute(IWorkUnitModel input, ref IWorkUnitModel siteParserResult)
        {
            ((ParserExecutorResult)siteParserResult).Prop2 = "WorkUnit_2_A set Prop2";
            return new SiteAModelB();
        }
    }
}

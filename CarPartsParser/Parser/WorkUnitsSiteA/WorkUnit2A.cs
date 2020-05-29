using CarPartsParser.Abstraction.Models;
using CarPartsParser.Abstraction.Services;
using CarPartsParser.Models;
using CarPartsParser.Models.SiteA;

namespace CarPartsParser.Parser.WorkUnitsSiteA
{
    public class WorkUnit2A : WorkUnitBaseA
    {
        public WorkUnit2A(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override IWorkUnitModel Execute(IWorkUnitModel input, ref ParserExecutorResultBase siteParserResult)
        {
            var serviceB = serviceProvider.GetServiceB();

            var e = (ParserExecutorResult)siteParserResult;
            e.Zzz = 42;

            siteParserResult.Prop2 = "WorkUnit_2_A set Prop2; " + serviceB.ExecuteServiceB();

            return new SiteAModelB();
        }
    }
}

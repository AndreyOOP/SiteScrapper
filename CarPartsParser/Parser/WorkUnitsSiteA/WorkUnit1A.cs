using CarPartsParser.Abstraction.Models;
using CarPartsParser.Abstraction.Services;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Models;
using CarPartsParser.Models.SiteA;

namespace CarPartsParser.Parser.WorkUnitsSiteA
{
    public class WorkUnit1A : IWorkUnitA
    {
        private IServiceProvider serviceProvider;

        public WorkUnit1A(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IWorkUnitModel Execute(IWorkUnitModel input, ref IWorkUnitModel siteParserResult)
        {
            var serviceA = serviceProvider.GetServiceA();

            ((ParserExecutorResult)siteParserResult).Prop1 = "WorkUnit_1_A set Prop1; " + serviceA.ExecuteServiceA();

            return new SiteAModelA();
        }
    }
}

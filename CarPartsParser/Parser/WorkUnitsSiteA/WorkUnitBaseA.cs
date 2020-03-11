using CarPartsParser.Abstraction.Models;
using CarPartsParser.Abstraction.Services;
using CarPartsParser.Abstraction.WorkUtils;

namespace CarPartsParser.Parser.WorkUnitsSiteA
{
    // one way is to use absract clas with service provider, but not sure if it is really necessary
    // it is possible that WorkUnit does not need any service & it is not a big deal to inject it
    public abstract class WorkUnitBaseA : IWorkUnitA
    {
        internal IServiceProvider serviceProvider;

        public WorkUnitBaseA(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public abstract IWorkUnitModel Execute(IWorkUnitModel input, ref IWorkUnitModel siteParserResult);
    }
}

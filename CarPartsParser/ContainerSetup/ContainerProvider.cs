using CarPartsParser.Abstraction.Factories;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Factories;
using CarPartsParser.Parser.WorkUnitsSiteB;
using CarPartsParser.SiteParsers.RootParser;
using CarPartsParser.SiteParsers.SiteA;
using Unity;

namespace CarPartsParser.ContainerSetup
{
    public class ContainerProvider
    {
        private IUnityContainer container;

        public IUnityContainer Get()
        {
            if (container != null)
                return container;

            container = new UnityContainer();

            container.RegisterType<IWorkUnitA, WorkUnit1A>(nameof(WorkUnit1A));
            container.RegisterType<IWorkUnitA, WorkUnit2A>(nameof(WorkUnit2A));
            container.RegisterType<IWorkUnitB, WorkUnit1B>(nameof(WorkUnit1B));

            container.RegisterType<IWebSiteParsersFactory, WebSiteParsersFactory>();

            container.RegisterType<ParserExecutor>();

            return container;
        }
    }
}

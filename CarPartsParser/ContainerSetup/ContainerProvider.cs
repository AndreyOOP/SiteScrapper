using CarPartsParser.Abstraction.Factories;
using CarPartsParser.Abstraction.Services;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Factories;
using CarPartsParser.Parser.WorkUnitsSiteA;
using CarPartsParser.Parser.WorkUnitsSiteB;
using CarPartsParser.Services;
using CarPartsParser.SiteParsers.RootParser;
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

            container.RegisterType<IServiceA, ServiceA>();
            container.RegisterType<IServiceB, ServiceB>();
            container.RegisterType<IServiceProvider, ServiceProvider>();

            container.RegisterType<IWorkUnitA, WorkUnit1A>(nameof(WorkUnit1A));
            container.RegisterType<IWorkUnitA, WorkUnit2A>(nameof(WorkUnit2A));
            container.RegisterType<IWorkUnitB, WorkUnit1B>(nameof(WorkUnit1B));

            container.RegisterType<IWebSiteParsersFactory, WebSiteParsersFactory>();

            container.RegisterType<ParserExecutor>();

            return container;
        }
    }
}

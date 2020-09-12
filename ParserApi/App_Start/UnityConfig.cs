using ParserApi.Controllers;
using ParserApi.Parsers.Autoklad;
using ParserApi.Parsers.Autoklad.Models;
using ParserApi.Parsers.Autoklad.WorkUnits;
using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using ParserApi.Parsers.Site911ParserCore;
using ParserApi.Services;
using ParserCore;
using ParserCore.Abstraction;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Unity;
using Unity.Resolution;
using Unity.WebApi;
using Mvc = Unity.AspNet.Mvc;

namespace ParserApi
{
    public static class UnityConfig
    {
        static string Site911Name = nameof(Site911Parser);
        static string AutokladName = nameof(AutokladParser);

        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<HttpClient>();
            container.RegisterSingleton<ILoggerSettingsProvider, LoggerSettingsProvider>();
            container.RegisterFactory<WorkerLogSettings>(c => c.Resolve<ILoggerSettingsProvider>().Settings);

            ParserSite911Registration(container);
            ParserAutokladRegistration(container);

            container.RegisterFactory<ParsingController>(c => new ParsingController(
                c.Resolve<Site911Parser>(),
                c.Resolve<AutokladParser>()
            ));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            return container;
        }

        private static void ParserSite911Registration(UnityContainer container)
        {
            container.RegisterType<IInMemoryWorkerLogger, InMemoryWorkerLogger>(Site911Name, new Mvc.PerRequestLifetimeManager());

            container.RegisterType<IWorker<In911, HtmlStep1>, In911ToHtmlStep1>();
            container.RegisterType<IWorker<HtmlStep1, PrimaryResultStep2>, HtmlStep1ToPrimaryResultStep2>();
            container.RegisterType<IWorker<HtmlStep1, QueryStringStep2>, HtmlStep1ToQueryStringStep2>();
            container.RegisterType<IWorker<QueryStringStep2, HtmlStep3>, QueryStringStep2ToHtmlStep3>();
            container.RegisterType<IWorker<HtmlStep3, SecondaryResultStep4>, HtmlStep3ToSecondaryResultStep4>();

            container.RegisterType<LoggingWorker<In911, HtmlStep1>>();
            container.RegisterType<LoggingWorker<HtmlStep1, PrimaryResultStep2>>();
            container.RegisterType<LoggingWorker<HtmlStep1, QueryStringStep2>>();
            container.RegisterType<LoggingWorker<QueryStringStep2, HtmlStep3>>();
            container.RegisterType<LoggingWorker<HtmlStep3, SecondaryResultStep4>>();

            container.RegisterFactory<IWorkersContainer>(Site911Name, c =>
            {
                var site911Logger = new DependencyOverride<IInMemoryWorkerLogger>(c.Resolve<IInMemoryWorkerLogger>(Site911Name));
                var parsingGraph = new Dictionary<IInOutKey, object>
                {
                    [new InOutKey<In911, HtmlStep1>()] = c.Resolve<LoggingWorker<In911, HtmlStep1>>(site911Logger),
                    [new InOutKey<HtmlStep1, PrimaryResultStep2>()] = c.Resolve<LoggingWorker<HtmlStep1, PrimaryResultStep2>>(site911Logger),
                    [new InOutKey<HtmlStep1, QueryStringStep2>()] = c.Resolve<LoggingWorker<HtmlStep1, QueryStringStep2>>(site911Logger),
                    [new InOutKey<QueryStringStep2, HtmlStep3>()] = c.Resolve<LoggingWorker<QueryStringStep2, HtmlStep3>>(site911Logger),
                    [new InOutKey<HtmlStep3, SecondaryResultStep4>()] = c.Resolve<LoggingWorker<HtmlStep3, SecondaryResultStep4>>(site911Logger),
                };
                return new WorkersContainer(parsingGraph);
            });

            container.RegisterType<Site911Parser>();
        }

        private static void ParserAutokladRegistration(UnityContainer container)
        {
            container.RegisterType<IInMemoryWorkerLogger, InMemoryWorkerLogger>(AutokladName, new Mvc.PerRequestLifetimeManager());

            container.RegisterType<IWorker<InAK, HtmlS1AK>, InToHtmlS1AK>();

            container.RegisterType<LoggingWorker<InAK, HtmlS1AK>>();

            container.RegisterFactory<IWorkersContainer>(AutokladName, c =>
            {
                var autokladLogger = new DependencyOverride<IInMemoryWorkerLogger>(c.Resolve<IInMemoryWorkerLogger>(AutokladName));
                var parsingGraph = new Dictionary<IInOutKey, object>
                {
                    [new InOutKey<InAK, HtmlS1AK>()] = c.Resolve<LoggingWorker<InAK, HtmlS1AK>>(autokladLogger),
                };
                return new WorkersContainer(parsingGraph);
            });

            container.RegisterType<AutokladParser>();
        }
    }
}
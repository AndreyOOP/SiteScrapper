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
using Unity.WebApi;
using Mvc = Unity.AspNet.Mvc;

namespace ParserApi
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<HttpClient>();
            container.RegisterSingleton<ILoggerSettingsProvider, LoggerSettingsProvider>();
            container.RegisterFactory<WorkerLogSettings>(c => c.Resolve<ILoggerSettingsProvider>().Settings);

            ParserSite911Registration(container);
            ParserAutokladRegistration(container);

            container.RegisterFactory<ParsingController>(c => new ParsingController(
                c.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Site911)),
                c.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Autoklad)),
                c.Resolve<Site911Parser>(),
                c.Resolve<AutokladParser>()
            ));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            return container;
        }

        private static void ParserSite911Registration(UnityContainer container)
        {
            container.RegisterType<IInMemoryWorkerLogger, InMemoryWorkerLogger>(nameof(SourceSite.Site911), new Mvc.PerRequestLifetimeManager());

            container.RegisterType<IWorker<In, HtmlStep1>, InToHtmlStep1>();
            container.RegisterType<IWorker<HtmlStep1, PrimaryResultStep2>, HtmlStep1ToPrimaryResultStep2>();
            container.RegisterType<IWorker<HtmlStep1, QueryStringStep2>, HtmlStep1ToQueryStringStep2>();
            container.RegisterType<IWorker<QueryStringStep2, HtmlStep3>, QueryStringStep2ToHtmlStep3>();
            container.RegisterType<IWorker<HtmlStep3, SecondaryResultStep4>, HtmlStep3ToSecondaryResultStep4>();

            container.RegisterFactory<LoggingWorker<In, HtmlStep1>>(c => new LoggingWorker<In, HtmlStep1>(c.Resolve<IWorker<In, HtmlStep1>>(), c.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Site911)), c.Resolve<WorkerLogSettings>()));
            container.RegisterFactory<LoggingWorker<HtmlStep1, PrimaryResultStep2>>(c => new LoggingWorker<HtmlStep1, PrimaryResultStep2>(c.Resolve<IWorker<HtmlStep1, PrimaryResultStep2>>(), c.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Site911)), c.Resolve<WorkerLogSettings>()));
            container.RegisterFactory<LoggingWorker<HtmlStep1, QueryStringStep2>>(c => new LoggingWorker<HtmlStep1, QueryStringStep2>(c.Resolve<IWorker<HtmlStep1, QueryStringStep2>>(), c.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Site911)), c.Resolve<WorkerLogSettings>()));
            container.RegisterFactory<LoggingWorker<QueryStringStep2, HtmlStep3>>(c => new LoggingWorker<QueryStringStep2, HtmlStep3>(c.Resolve<IWorker<QueryStringStep2, HtmlStep3>>(), c.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Site911)), c.Resolve<WorkerLogSettings>()));
            container.RegisterFactory<LoggingWorker<HtmlStep3, SecondaryResultStep4>>(c => new LoggingWorker<HtmlStep3, SecondaryResultStep4>(c.Resolve<IWorker<HtmlStep3, SecondaryResultStep4>>(), c.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Site911)), c.Resolve<WorkerLogSettings>()));

            container.RegisterFactory<IWorkersContainer>(nameof(SourceSite.Site911), c =>
            {
                var parsingGraph = new Dictionary<IInOutKey, object>
                {
                    [new InOutKey<In, HtmlStep1>()] = c.Resolve<LoggingWorker<In, HtmlStep1>>(),
                    [new InOutKey<HtmlStep1, PrimaryResultStep2>()] = c.Resolve<LoggingWorker<HtmlStep1, PrimaryResultStep2>>(),
                    [new InOutKey<HtmlStep1, QueryStringStep2>()] = c.Resolve<LoggingWorker<HtmlStep1, QueryStringStep2>>(),
                    [new InOutKey<QueryStringStep2, HtmlStep3>()] = c.Resolve<LoggingWorker<QueryStringStep2, HtmlStep3>>(),
                    [new InOutKey<HtmlStep3, SecondaryResultStep4>()] = c.Resolve<LoggingWorker<HtmlStep3, SecondaryResultStep4>>(),
                };
                return new WorkersContainer(parsingGraph);
            });

            container.RegisterFactory<Site911Parser>(c => new Site911Parser(c.Resolve<IWorkersContainer>(nameof(SourceSite.Site911))));
        }

        private static void ParserAutokladRegistration(UnityContainer container)
        {
            container.RegisterType<IInMemoryWorkerLogger, InMemoryWorkerLogger>(nameof(SourceSite.Autoklad), new Mvc.PerRequestLifetimeManager());

            container.RegisterType<IWorker<InAK, HtmlS1AK>, InToHtmlS1AK>();

            container.RegisterFactory<LoggingWorker<InAK, HtmlS1AK>>(c => new LoggingWorker<InAK, HtmlS1AK>(c.Resolve<IWorker<InAK, HtmlS1AK>>(), container.Resolve<IInMemoryWorkerLogger>(nameof(SourceSite.Autoklad)), container.Resolve<WorkerLogSettings>()));

            container.RegisterFactory<IWorkersContainer>(nameof(SourceSite.Autoklad), c =>
            {
                var parsingGraph = new Dictionary<IInOutKey, object>
                {
                    [new InOutKey<InAK, HtmlS1AK>()] = c.Resolve<LoggingWorker<InAK, HtmlS1AK>>(),
                };
                return new WorkersContainer(parsingGraph);
            });

            container.RegisterFactory<AutokladParser>(c => new AutokladParser(c.Resolve<IWorkersContainer>(nameof(SourceSite.Autoklad))));
        }
    }

    // ToDo: move out
    public enum SourceSite
    {
        Site911,
        Autoklad
    }
}
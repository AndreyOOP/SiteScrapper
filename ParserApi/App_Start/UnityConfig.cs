using ParserApi.Parsers.Site911.Models;
using ParserApi.Parsers.Site911.WorkUnits;
using ParserApi.Parsers.Site911ParserCore;
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

            container.RegisterType<WorkerLogSettings, ExceptionLoggerSettings>(new Mvc.PerRequestLifetimeManager());
            container.RegisterType<IInMemoryWorkerLogger, InMemoryWorkerLogger>(new Mvc.PerRequestLifetimeManager());
            container.RegisterType<HttpClient>();

            RegisterWorkers(container);
            RegisterWorkersContainer(container);

            container.RegisterType<Site911Parser>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            return container;
        }

        private static void RegisterWorkers(UnityContainer container)
        {
            container.RegisterType<IWorker<In, HtmlStep1>, InToHtmlStep1>();
            container.RegisterType<IWorker<HtmlStep1, PrimaryResultStep2>, HtmlStep1ToPrimaryResultStep2>();
            container.RegisterType<IWorker<HtmlStep1, QueryStringStep2>, HtmlStep1ToQueryStringStep2>();
            container.RegisterType<IWorker<QueryStringStep2, HtmlStep3>, QueryStringStep2ToHtmlStep3>();
            container.RegisterType<IWorker<HtmlStep3, SecondaryResultStep4>, HtmlStep3ToSecondaryResultStep4>();

            container.RegisterType<LoggingWorker<In, HtmlStep1>>();
            container.RegisterType<LoggingWorker<HtmlStep1, PrimaryResultStep2>>();
            container.RegisterType<LoggingWorker<HtmlStep1, QueryStringStep2>>();
            container.RegisterType<LoggingWorker<QueryStringStep2, HtmlStep3>>();
            container.RegisterType<LoggingWorker<HtmlStep3, SecondaryResultStep4>>();
        }

        private static void RegisterWorkersContainer(UnityContainer container)
        {
            container.RegisterFactory<IWorkersContainer>(c =>
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
        }
    }
}
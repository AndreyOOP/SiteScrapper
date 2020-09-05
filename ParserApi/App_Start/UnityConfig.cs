using ParserApi.Parsers.Site911.Models;
using ParserCore;
using ParserCore.Abstraction;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ParserApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<WorkerLogSettings, ExceptionLoggerSettings>();
            container.RegisterType<ILogger<WorkerLogRecord>, InMemoryWorkerLogger>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
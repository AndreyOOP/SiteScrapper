using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.SiteParsers.RootParser;
using CarPartsParser.SiteParsers.SiteA;
using CarPartsParser.SiteParsers.SiteB;
using System;
using System.Collections.Generic;
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

            container.RegisterType<IWebSiteParser, WebSiteParserA>(nameof(WebSiteParserA));
            container.RegisterType<IWebSiteParser, WebSiteParserB>(nameof(WebSiteParserB));
            container.RegisterType<RootParser>();

            var adapters = new Dictionary<Type, IResultAdapter<IParsedResult>> { 
                [typeof(ResultA)] = new AdapterA(),
                [typeof(ResultB)] = new AdapterB()
            };
            container.RegisterInstance(adapters);

            return container;
        }
    }
}

using CarPartsParser.Abstraction.Factories;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Parser;
using CarPartsParser.Parser.WorkUnitsSiteB;
using CarPartsParser.SiteParsers.Abstraction;
using CarPartsParser.SiteParsers.SiteA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPartsParser.Factories
{
    public class WebSiteParsersFactory : IWebSiteParsersFactory
    {
        private Dictionary<Type, IWorkUnitA> workUnitsA;
        private Dictionary<Type, IWorkUnitB> workUnitsB;

        public WebSiteParsersFactory(IEnumerable<IWorkUnitA> workUnitsA, IEnumerable<IWorkUnitB> workUnitsB)
        {
            this.workUnitsA = workUnitsA.ToDictionary(k => k.GetType(), v => v);
            this.workUnitsB = workUnitsB.ToDictionary(k => k.GetType(), v => v);
        }

        public IEnumerable<IWebSiteParser> GetAll()
        {
            var parsers = new List<IWebSiteParser> 
            { 
                GetParserA(), 
                GetParserB()
            };
            return parsers;
        }

        private WebSiteParserA GetParserA()
        {
            var parser = new WebSiteParserA();

            parser.RegisterUnit(workUnitsA[typeof(WorkUnit1A)]);
            parser.RegisterUnit(workUnitsA[typeof(WorkUnit2A)]);

            return parser;
        }

        private WebSiteParserB GetParserB()
        {
            var parser = new WebSiteParserB();

            parser.RegisterUnit(workUnitsB[typeof(WorkUnit1B)]);

            return parser;
        }
    }
}

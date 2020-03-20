using CarPartsParser.Abstraction.Factories;
using CarPartsParser.Abstraction.WorkUtils;
using CarPartsParser.Models;
using CarPartsParser.Parser;
using CarPartsParser.Parser.Tree;
using CarPartsParser.Parser.WorkUnitsSiteA;
using CarPartsParser.Parser.WorkUnitsSiteB;
using CarPartsParser.SiteParsers.Abstraction;
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

        public IEnumerable<IWebSiteParser<ParserExecutorResultBase>> GetAll()
        {
            var parsers = new List<IWebSiteParser<ParserExecutorResultBase>> 
            { 
                GetParserA(), 
                GetParserB()
            };
            return parsers;
        }

        private WebSiteParserA GetParserA()
        {
            var tree = new WorkUnitTree(workUnitsA[typeof(WorkUnit1A)]);
            tree.AddNextNode(new WorkUnitTree(workUnitsA[typeof(WorkUnit2A)]));

            return new WebSiteParserA(tree);
        }

        private WebSiteParserB GetParserB()
        {
            var tree = new WorkUnitTree(workUnitsB[typeof(WorkUnit1B)]);

            return new WebSiteParserB(tree);
        }
    }
}

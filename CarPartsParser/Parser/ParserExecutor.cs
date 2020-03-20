using CarPartsParser.Abstraction.Factories;
using CarPartsParser.Abstraction.Models;
using CarPartsParser.Models;
using System.Collections.Generic;

namespace CarPartsParser.SiteParsers.RootParser
{
    public class ParserExecutor
    {
        private IWebSiteParsersFactory factory;

        public ParserExecutor(IWebSiteParsersFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<ParserExecutorResultBase> Parse(IWorkUnitModel id)
        {
            var result = new List<ParserExecutorResultBase>();

            foreach (var parser in factory.GetAll())
            {
                var siteResult = parser.Parse(id);
                result.Add(siteResult);
            }

            return result;
        }
    }
}

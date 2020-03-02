using CarPartsParser.SiteParsers.Abstraction;
using System;
using System.Collections.Generic;

// SingleResponsibility - ?
// ToDo: adapter should be part of WebSiteParser - in implementation above it is possible to create not aligned siteParsers & adapters
namespace CarPartsParser.SiteParsers.RootParser
{
    public class RootParser
    {
        private IEnumerable<IWebSiteParser> siteParsers;
        private Dictionary<Type, IResultAdapter<IParsedResult>> adapters;

        public RootParser(IEnumerable<IWebSiteParser> siteParsers, Dictionary<Type, IResultAdapter<IParsedResult>> adapters)
        {
            this.siteParsers = siteParsers;
            this.adapters = adapters;
        }

        public IEnumerable<RootResult> Parse(string id)
        {
            var result = new List<RootResult>();

            foreach (var parser in siteParsers)
            {
                var siteResult = parser.Parse(id);

                var converter = adapters[siteResult.GetType()]; // no sence in adapter it could be just additional step in WebSiteParser
                var converted = converter.Convert(siteResult);

                result.Add(converted);
            }

            return result;
        }
    }
}

using CarPartsParser.SiteParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsParser
{
    public class MainParser
    {
        private IEnumerable<ISiteParser> siteParsers;
        private IEnumerable<IResultAdapter<IParsedResult>> adapters;
        private Dictionary<Type, IResultAdapter<IParsedResult>> adaptersDictionary;

        // just register type of ISiteParser | IResultAdapter => it will be used here
        // as well it is possible to inject strategy here, to in case of complex logic / conditions which parsers to use
        public MainParser(IEnumerable<ISiteParser> siteParsers, IEnumerable<IResultAdapter<IParsedResult>> adapters)
        {
            this.siteParsers = siteParsers;
            this.adapters = adapters;

            adaptersDictionary = adapters.ToDictionary(
                k => k.GetType().GetInterfaces().First(i => i.Name.Contains(nameof(IResultAdapter<IParsedResult>))).GetGenericArguments().First(), 
                v => v);
        }

        public IEnumerable<IParsedResult> Parse(string id) // maybe input could be not just id - but more complex object
        {
            var result = new List<IParsedResult>();

            foreach (var parser in siteParsers)
            {
                var siteResult = parser.Parse(id); // have to be done in ||

                var converter = adaptersDictionary[siteResult.GetType()];
                var converted = converter.Convert(siteResult);

                result.Add(converted);
            }

            return result;
        }
    }

    public abstract class SiteParser<T> // return type of parse result
    {
        internal T result;

        public abstract T Parse(string id);
    }

    // Builder main parser - register site parser
    // register strategy attempts - ?
}

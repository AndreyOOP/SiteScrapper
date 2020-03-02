using CarPartsParser.SiteParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CarPartsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();

            container.RegisterType<ISiteParser, SiteParserA>("1"); // ToDo: check collection registration
            container.RegisterType<ISiteParser, SiteParserB>("2");
            container.RegisterType(typeof(IResultAdapter<>), typeof(AdapterA));
            container.RegisterType<MainParser>();

            var mainParser = container.Resolve<MainParser>();
        }
    }

    //Object contain list of SiteParsers - it return result for each site
}

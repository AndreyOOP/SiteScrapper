using CarPartsParser.Abstraction.Models;
using CarPartsParser.Models;
using CarPartsParser.SiteParsers.Abstraction.WorkUnits;
using System.Collections.Generic;

namespace CarPartsParser.SiteParsers.Abstraction
{
    public class WebSiteParser<TUnit> : IWebSiteParser where TUnit : IWorkUnit
    {
        private Queue<IWorkUnit> queue = new Queue<IWorkUnit>();

        public IWorkUnitModel Parse(IWorkUnitModel input)
        {
            var model = input;
            IWorkUnitModel siteResult = new ParserExecutorResult();

            foreach (var unit in queue)
            {
                if (((ParserExecutorResult)siteResult).Exception != null)
                {
                    break;
                }
                model = unit.Execute(model, ref siteResult);
            }
            return siteResult;
        }

        public void RegisterUnit(TUnit unit)
        {
            queue.Enqueue(unit);
        }
    }
}

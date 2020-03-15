using CarPartsParser.Abstraction.Models;
using CarPartsParser.Models;
using CarPartsParser.Parser.Tree;
using CarPartsParser.SiteParsers.Abstraction.WorkUnits;

namespace CarPartsParser.SiteParsers.Abstraction
{
    public class WebSiteParser<TUnit> : IWebSiteParser where TUnit : IWorkUnit
    {
        private WorkUnitTree tree;

        public WebSiteParser(WorkUnitTree tree)
        {
            this.tree = tree;
        }

        public IWorkUnitModel Parse(IWorkUnitModel input)
        {
            var node = tree;
            var model = input;
            IWorkUnitModel siteResult = new ParserExecutorResult();

            for(;;)
            {
                if (((ParserExecutorResult)siteResult).Exception != null)
                {
                    break;
                }

                var modelBeforeUpdate = model;

                model = node.Unit.Execute(model, ref siteResult);

                if (node.IsLastNode())
                    break;

                node = node.NextNode(modelBeforeUpdate, model);
            }

            return siteResult;
        }
    }
}

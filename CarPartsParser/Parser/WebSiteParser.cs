using CarPartsParser.Abstraction.Models;
using CarPartsParser.Models;
using CarPartsParser.Parser.Tree;
using CarPartsParser.SiteParsers.Abstraction.WorkUnits;
using Newtonsoft.Json;
using System;

namespace CarPartsParser.SiteParsers.Abstraction
{
    public class WebSiteParser<TUnit, TOut> : IWebSiteParser<TOut> 
        where TUnit : IWorkUnit 
        where TOut : ParserExecutorResultBase, new()
    {
        private WorkUnitTree tree;

        public WebSiteParser(WorkUnitTree tree)
        {
            this.tree = tree;
        }

        public TOut Parse(IWorkUnitModel input)
        {
            var node = tree;
            var model = input;
            var siteResult = (ParserExecutorResultBase)new TOut();

            for(;;)
            {
                if (siteResult.Exception != null)
                {
                    break;
                }

                var modelBeforeUpdate = model;

                try
                {
                    siteResult.ExecutionPath += $"{node.Unit.GetType().Name} > ";

                    model = node.Unit.Execute(model, ref siteResult);
                    
                    if (node.IsLastNode())
                        break;

                    node = node.NextNode(modelBeforeUpdate, model);
                }
                catch (Exception ex)
                {
                    siteResult.Exception = JsonConvert.SerializeObject(new
                    {
                        UnitName = node.Unit.GetType().Name,
                        Message = ex.Message,
                        Path = siteResult.ExecutionPath
                    });
                }
            }

            return (TOut)siteResult;
        }
    }
}

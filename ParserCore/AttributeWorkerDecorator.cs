using ParserCore.Abstraction;
using ParserCore.Attributes;
using ParserCore.Extensions;

namespace ParserCore
{
    public class AttributeWorkerDecorator<TIn, TOut> : IWorker<TIn, TOut>
        where TIn : IHtmlNodeProvider
        where TOut : new()
    {
        private IWorker<TIn, TOut> worker;

        /// <summary>
        /// TOut model produced by <param name="worker"> will be updated according to <see cref="XPathAttribute"/>
        /// </summary>
        /// <param name="worker">Worker to be decorated</param>
        public AttributeWorkerDecorator(IWorker<TIn, TOut> worker)
        {
            this.worker = worker;
        }

        public bool IsExecutable(TIn model)
            => worker.IsExecutable(model);

        public TOut Parse(TIn model)
        {
            var htmlNode = model.Node;
            var tOut = worker.Parse(model) ?? new TOut();

            foreach(var property in tOut.GetType().GetProperties())
            {
                var xPath = property.GetCustomAttributeData<XPathAttribute>()
                                    ?.GetConstructorArgument<string>();
                if(xPath != null)
                {
                    var value = htmlNode.SelectSingleNode(xPath)?.InnerText;
                    // ToDo: convert to property type
                    property.SetValue(tOut, value);
                }
            }

            return tOut;
        }
    }
}

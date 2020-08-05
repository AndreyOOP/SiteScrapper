using ParserCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCore
{
    /// <summary>
    /// Final parsing result has to be stored into TParsingResult => last Worker has to create this model. 
    /// If <see cref="TParsingResult"/> is complex model which consists of different parts - define TParsingResult model creation logic in PrepareResult method
    /// </summary>
    public class ParserBase<TIn, TParsingResult> : IParserBase<TIn, TParsingResult>
    {
        private readonly IWorkersContainer workersContainer;

        /// <summary>
        /// During parsing any partial result stored here, after all final result gathererd together in PrepareResult
        /// </summary>
        protected Dictionary<Type, object> resultMain = new Dictionary<Type, object>();

        public ParserBase(IWorkersContainer workersContainer)
        {
            this.workersContainer = workersContainer;
        }
        
        /// <inheritdoc/>
        public TParsingResult Parse(TIn model)
        {
            GetNext(new[] {
                new TypeToModel { Type = typeof(TIn), Model = model }
            });

            return PrepareResult();
        }

        protected void GetNext(IEnumerable<TypeToModel> models)
        {
            var result = new List<TypeToModel>();

            foreach (var model in models)
            {
                var workers = workersContainer.Get(model.Type);

                foreach (var worker in workers)
                {
                    var mi1 = worker.Worker.GetType().GetMethod("ToExecute");
                    if (!(bool)mi1.Invoke(worker.Worker, new object[] { model.Model }))
                        continue;

                    var methodInfo = worker.Worker.GetType().GetMethod("Parse"); //nameof(IWorkerBase.Parse)
                    var outModel = methodInfo.Invoke(worker.Worker, new object[] { model.Model });

                    result.Add(new TypeToModel
                    {
                        Type = worker.Key.OutType,
                        Model = outModel
                    });
                }
            }

            var last = result.Where(r => workersContainer.Last.Any(x => x.OutType == r.Type)).ToDictionary(k => k.Type, v => v.Model);
            resultMain = resultMain.Concat(last).ToDictionary(k => k.Key, v => v.Value);

            var next = result.Where(r => workersContainer.Last.All(x => x.OutType != r.Type));
            if (next.Count() > 0)
                GetNext(next);
        }

        /// <summary>
        /// By default just try to get result as <see cref="TParsingResult"/> type
        /// </summary>
        protected virtual TParsingResult PrepareResult() => resultMain.Get<TParsingResult>();
    }
}

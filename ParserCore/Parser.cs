using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCore
{
    public class Parser<TIn, TOut>
    {
        private IWorkersContainer workersContainer;

        /// <summary>
        /// During parsing any partial result stored here, after all final result gathererd together in PrepareResult
        /// </summary>
        protected Dictionary<Type, object> resultMain;

        public Parser(IWorkersContainer workersContainer)
        {
            this.workersContainer = workersContainer;
            resultMain = new Dictionary<Type, object>();
        }

        public TOut Parse(TIn model)
        {
            GetNext(new[] {
                new TypeToModel { Type = typeof(TIn), Model = model }
            });
            
            return PrepareResult();
        }

        protected void GetNext(IEnumerable<TypeToModel> models)
        {
            var result = new List<TypeToModel>();

            foreach(var model in models)
            {
                var workers = workersContainer.Get(model.Type);

                foreach(var worker in workers)
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
        /// By default just try to get result as TOut type
        /// </summary>
        /// <returns></returns>
        protected virtual TOut PrepareResult()
        {
            return (TOut)resultMain[typeof(TOut)];
        }
    }

    
}

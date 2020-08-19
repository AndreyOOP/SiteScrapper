using ParserCore.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ParserCore
{
    /// <summary>
    /// Final parsing result has to be stored into TParsingResult => last Worker has to create this model. 
    /// If <see cref="TParsingResult"/> is complex model which consists of different parts - define TParsingResult model creation logic in PrepareResult method
    /// </summary>
    public class ParserBase<TIn, TParsingResult> : IParser<TIn, TParsingResult>
    {
        private const string IsExecutableMethod = nameof(IWorker<object, object>.IsExecutable);
        private const string ParseMethod = nameof(IWorker<object, object>.Parse);

        private readonly IWorkersContainer workersContainer;

        /// <summary>
        /// During parsing any partial result stored here, after all final result gathererd together in PrepareResult
        /// </summary>
        protected IEnumerable<object> result = new List<object>();

        public ParserBase(IWorkersContainer workersContainer)
        {
            this.workersContainer = workersContainer;
        }
        
        /// <inheritdoc/>
        public TParsingResult Parse(TIn model)
        {
            GetFinalTOutModels(new object[] { model });

            return PrepareResult();
        }

        /// <summary>
        /// Find workers based on TIn model types - execute them, if it is last worker add TOut models to result, otherwise find next workers & execute them - repeat it recursively
        /// </summary>
        /// <param name="tInTypes">TIn models for which Workers<TIn, TOut> will be executed</param>
        protected void GetFinalTOutModels(IEnumerable<object> inTypes)
        {
            var outTypes = inTypes.SelectMany
            (
                inType => workersContainer.GetWorkers(inType.GetType())
                                       .Where(worker => (bool)worker.InvokeMethod(IsExecutableMethod, inType))
                                       .Select(worker => worker.InvokeMethod(ParseMethod, inType))
            );
            
            var splitted = SplitToFinalTypesAndInput(outTypes);
           
            result = result.Concat(splitted.FinalTypes);

            if (splitted.InputTypes.Count() > 0)
            {
                GetFinalTOutModels(splitted.InputTypes);
            }
        }

        protected FinalAndInputTypes SplitToFinalTypesAndInput(IEnumerable<object> outTypes)
        {
            var groups = outTypes.GroupBy(outType => workersContainer.Last.Any(w => w.OutType == outType.GetType()));

            return new FinalAndInputTypes
            {
                FinalTypes = groups.FirstOrDefault(group => group.Key)?.ToList() ?? new List<object>(),
                InputTypes = groups.FirstOrDefault(group => !group.Key)?.ToList() ?? new List<object>()
            };
        }

        /// <summary>
        /// By default just try to get result as <see cref="TParsingResult"/> type
        /// </summary>
        protected virtual TParsingResult PrepareResult() => result.Get<TParsingResult>();
    }

}
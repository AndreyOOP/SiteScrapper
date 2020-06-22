////Note: this SelectWorkUnit knows which WorkUnit has to be executed next - so maybe better insted of SelectWorkUnit use SetWorkUnit - so it will be provided
//using System;

//namespace SiteParsingHelper.Event.Abstraction
//{
//    /// <summary>
//    /// Base class for WorkUnit - step of parsing process. 
//    /// It get <see cref="TOut"/> model based on <see cref="TIn"/> input and then decides which WorkUnit has to be executed next
//    /// </summary>
//    /// <typeparam name="TIn">Input model</typeparam>
//    /// <typeparam name="TOut">Result creatred based on input model</typeparam>
//    /// <typeparam name="TParserResult">Final result. This model could be updated on each step</typeparam>
//    [Obsolete]
//    public abstract class WorkUnitBase<TIn, TOut, TParserResult> : IUnit<TIn>
//    {
//        protected IWebParser<TParserResult> webParser;

//        public WorkUnitBase(IWebParser<TParserResult> webParser)
//        {
//            this.webParser = webParser;
//        }

//        /// <summary>
//        /// Based on TIn model gets TOut model (http request, parsing data etc)
//        /// </summary>
//        protected abstract TOut ParseUnit(TIn model);

//        /// <summary>
//        /// Based on <see cref="ParseUnit(TIn)"/> execution result select next WorkUnit
//        /// If it is the last step - just return
//        /// </summary>
//        /// <param name="model"><see cref="ParseUnit(TIn)"/> result/param>
//        protected abstract void SelectWorkUnit(TOut model);

//        /// <summary>
//        /// It get <see cref="TOut"/> model based on <see cref="TIn"/> input and then call next WorkUnit
//        /// </summary>
//        /// <param name="model"></param>
//        public void ParseAndSelectNext(TIn model)
//        {
//            TOut outResult = ParseUnit(model);

//            SelectWorkUnit(outResult);
//        }
//    }
//}

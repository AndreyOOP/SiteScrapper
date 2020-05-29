using SiteParsingHelper.Event.Abstraction;
using System;
using System.Collections.Generic;

// Note: it is rather event bus/event broker
// Currently for correct work each request should be new WebParser - for execution path to be correct
// ToDo: avoid recreation of class | make it simpler - path dictionary into ctor
//       chage to async
//       maybe should be addition wrapper like site911 parser which uses internally WebParser<>
namespace ParserCoreProject.ParserCore
{
    public class WebParser<TResult> : IWebParser<TResult> 
        where TResult : new()
    {
        public TResult Result { get; }
        public ExecutionPath ExecutionPath { get; }
        public Exception Exception { get; private set; }

        private Dictionary<Tuple<Type, Type>, object> workUnits;

        public WebParser()
        {
            workUnits = new Dictionary<Tuple<Type, Type>, object>();
            Result = new TResult();
            ExecutionPath = new ExecutionPath();
        }

        public void RegisterUnit<TIn, TOut>(IUnit<TIn> workUnit)
        {
            if (workUnits.ContainsKey(Key<TIn, TOut>()))
                throw new ArgumentException($"Handler for input model '{typeof(TIn).Name}' and output model '{typeof(TOut).Name}' already set");

            workUnits.Add(Key<TIn, TOut>(), workUnit);
        }

        // ToDo: public method - it should be protected, web parser user should not know starting unit - it has to be registered & only mehod startparcing should be available
        public void ExecuteUnit<TIn, TOut>(TIn handlerInputModel)
        {
            try
            {
                if (!workUnits.ContainsKey(Key<TIn, TOut>()))
                    throw new ArgumentException($"Handler for input model '{typeof(TIn).Name}' and output model '{typeof(TOut).Name}' is not registered");

                var workUnitObject = workUnits[Key<TIn, TOut>()];

                var workUnit = workUnitObject as IUnit<TIn>;

                if (workUnit == null)
                    throw new Exception("Unexpected type");

                var workUnitName = workUnit.GetType().Name;

                if (ExecutionPath.AlreadyExecuted(workUnitName))
                    throw new Exception("Cycle found during execution");

                ExecutionPath.Add(workUnitName);

                workUnit.ParseAndSelectNext(handlerInputModel);
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }

        private Tuple<Type, Type> Key<TIn, TOut>() => new Tuple<Type, Type>(typeof(TIn), typeof(TOut));
    }
}


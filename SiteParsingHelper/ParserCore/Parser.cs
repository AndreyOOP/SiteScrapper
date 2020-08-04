using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCoreProject.ParserCore
{
    // parser prototype
    public class Parser<TResult>
    {
        //private WorkersChain workersChain;

        //public Parser(WorkersChain workersChain)
        //{
        //    this.workersChain = workersChain;
        //}

        //public TResult Execute(IEnumerable<object> models)
        //{
        //    var list = new List<object>();

        //    foreach(var m in models)
        //    {
        //        list.AddRange(
        //            workersChain.Get(m.GetType()).Select(w => w.Item2.Parse<>(w))
        //        );
        //    }

        //    Execute(list);
        //}
    }
    //    private WorkerSharedServices<TIn, TFirstOut,TResult> workerSharedServices;

    //    public Parser(WorkerSharedServices<TIn, TFirstOut, TResult> workerSharedServices)
    //    {
    //        this.workerSharedServices = workerSharedServices;
    //    }

    //    // set first | set start point || move to constructor
    //    // as well set stop workunits - ? - seems ok

    //    public TResult Execute(TIn model)
    //    {
    //        try
    //        {
    //            var worker = workerSharedServices.WorkersContainer.GetFirst();
    //            worker.ParseAndExecuteNext(model);
    //            return workerSharedServices.Result;
    //        }
    //        catch (Exception ex)
    //        {
    //            // add to exception path info, rethrow
    //            throw ex;
    //        }
    //    }
    //}
}

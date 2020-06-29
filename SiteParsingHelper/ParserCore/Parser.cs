using System;

namespace ParserCoreProject.ParserCore
{
    // parser prototype
    //public class Parser<TIn, TFirstOut, TResult>
    //{
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

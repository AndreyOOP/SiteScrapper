using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCore
{
    public interface IWorker<TIn, TOut>
    {
        TOut Parse(TIn model);

        bool ToExecute(TIn model);
    }

    //public interface IWorkerBase
    //{
    //    object Parse(object model);

    //    void z();
    //}

    //public abstract class WorkerBase<TIn, TOut> : IWorkerBase
    //{
    //    public TOut Parse(TIn model)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

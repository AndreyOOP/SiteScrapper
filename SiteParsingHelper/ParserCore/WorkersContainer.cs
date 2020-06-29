using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCoreProject.ParserCore
{
    /// <summary>
    /// IWorkersContainer implementation. Allows to register & get workers
    /// </summary>
    /// <typeparam name="TFirstIn">In type of first worker. Parsing begins from this worker</typeparam>
    /// <typeparam name="TFirstOut">Out type of first worker. Parsing begins from this worker</typeparam>
    public class WorkersContainer<TFirstIn, TFirstOut> : IWorkersContainer // ToDo: remove generic
    {
        protected Dictionary<IInOutKey, object> workers = new Dictionary<IInOutKey, object>();

        public void Add<TIn, TOut>(IWorker<TIn, TOut> worker)
        {
            var key = new InOutKey<TIn, TOut>();

            if (workers.ContainsKey(key))
                throw new ArgumentException(string.Format(Resource.WorkerAlreadySet, typeof(TIn).Name, typeof(TOut).Name));

            workers.Add(key, worker);
        }

        public IWorker<TIn, TOut> Get<TIn, TOut>()
        {
            var key = new InOutKey<TIn, TOut>();

            if (!workers.ContainsKey(key))
                throw new ArgumentException(string.Format(Resource.WorkerIsNotRegistered, typeof(TIn).Name, typeof(TOut).Name));

            var workerObject = workers[key];
            var worker = workerObject as IWorker<TIn, TOut>;

            if (worker == null)
                throw new InvalidCastException(string.Format(Resource.UnknownWorkerType, typeof(IWorker<TIn, TOut>).Name));

            return worker;
        }

        // ToDo: remove - ?
        public dynamic GetIfSingleImplementation<TIn>()
        {
            var qtyOfWorkersWithInputTypeTIn = workers.Count(w => w.Key.InType == typeof(TIn));

            if (qtyOfWorkersWithInputTypeTIn == 0)
                throw new ArgumentException(string.Format(Resource.WorkerIsNotRegistered, typeof(TFirstIn).Name, "AnyOtherType"));

            if (qtyOfWorkersWithInputTypeTIn > 1)
                throw new ArgumentException(string.Format(Resource.WorkerHasFewImplementation, typeof(TIn)));

            return workers.First(w => w.Key.InType == typeof(TIn)).Value;
        }
    }
}

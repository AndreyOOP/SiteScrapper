using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserCoreProject.ParserCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFirstIn">In type of first worker. Parsing begins from this worker</typeparam>
    /// <typeparam name="TFirstOut">Out type of first worker. Parsing begins from this worker</typeparam>
    public class WorkersContainer<TFirstIn, TFirstOut>
    {
        protected Dictionary<Tuple<Type, Type>, object> workers = new Dictionary<Tuple<Type, Type>, object>();

        public void Add<TIn, TOut>(IWorker<TIn, TOut> worker)
        {
            if (workers.ContainsKey(Key<TIn, TOut>()))
                throw new ArgumentException(string.Format(Resource.WorkerAlreadySet,  typeof(TIn).Name, typeof(TOut).Name));

            workers.Add(Key<TIn, TOut>(), worker);
        }

        public IWorker<TFirstIn, TFirstOut> GetFirst()
        {
            if (!workers.ContainsKey(Key<TFirstIn, TFirstOut>()))
                throw new ArgumentException(string.Format(Resource.WorkerIsNotRegistered, typeof(TFirstIn).Name, typeof(TFirstOut).Name));

            var workerObject = workers[Key<TFirstIn, TFirstOut>()];
            var worker = workerObject as IWorker<TFirstIn, TFirstOut>;

            if (worker == null)
                throw new InvalidCastException(string.Format(Resource.UnknownWorkerType, typeof(IWorker<TFirstIn, TFirstOut>).Name));

            return worker;
        }

        public IWorker<TIn, TOut> Get<TIn, TOut>()
        {
            if (!workers.ContainsKey(Key<TIn, TOut>()))
                throw new ArgumentException(string.Format(Resource.WorkerIsNotRegistered, typeof(TFirstIn).Name, typeof(TFirstOut).Name));

            var workerObject = workers[Key<TIn, TOut>()];
            var worker = workerObject as IWorker<TIn, TOut>;

            if (worker == null)
                throw new InvalidCastException(string.Format(Resource.UnknownWorkerType, typeof(IWorker<TIn, TOut>).Name));
            
            return worker;
        }

        public dynamic GetIfSingleImplementation<TIn>()
        {
            var qty = workers.Count(w => w.Key.Item1 == typeof(TIn));

            if(qty == 0)
                throw new ArgumentException(string.Format(Resource.WorkerIsNotRegistered, typeof(TFirstIn).Name, "AnyOtherType"));

            if (qty > 1)
                throw new ArgumentException(string.Format(Resource.WorkerHasFewImplementation, typeof(TIn)));

            return workers.First(w => w.Key.Item1 == typeof(TIn)).Value;
        }

        private Tuple<Type, Type> Key<TIn, TOut>() => new Tuple<Type, Type>(typeof(TIn), typeof(TOut));
    }
}

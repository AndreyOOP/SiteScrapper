using ParserCoreProject.Abstraction;
using System;
using System.Collections.Generic;

namespace ParserCoreProject.ParserCore
{
    public class WorkersContainer<TStartIn, TStartOut>
    {
        protected Dictionary<Tuple<Type, Type>, object> workers = new Dictionary<Tuple<Type, Type>, object>();

        public void Add<TIn, TOut>(IWorker<TIn, TOut> worker)
        {
            if (workers.ContainsKey(Key<TIn, TOut>()))
                throw new ArgumentException(string.Format(Resource.WorkerAlreadySet,  typeof(TIn).Name, typeof(TOut).Name));

            workers.Add(Key<TIn, TOut>(), worker);
        }

        public IWorker<TStartIn, TStartOut> GetFirst()
        {
            if (!workers.ContainsKey(Key<TStartIn, TStartOut>()))
                throw new ArgumentException(string.Format(Resource.WorkerAlreadySet, typeof(TStartIn).Name, typeof(TStartOut).Name));

            var workerObject = workers[Key<TStartIn, TStartOut>()];
            var worker = workerObject as IWorker<TStartIn, TStartOut>;

            if (worker == null)
                throw new InvalidCastException(string.Format(Resource.UnknownWorkerType, typeof(IWorker<TStartIn, TStartOut>).Name));

            return worker;
        }

        private Tuple<Type, Type> Key<TIn, TOut>() => new Tuple<Type, Type>(typeof(TIn), typeof(TOut));
    }
}

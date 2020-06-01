﻿namespace ParserCoreProject.Abstraction
{
    public interface IWorkersContainer<TFirstIn, TFirstOut>
    {
        /// <summary>
        /// Adds IWorker<TIn, TOut> which converts TIn type to TOut type
        /// </summary>
        void Add<TIn, TOut>(IWorker<TIn, TOut> worker);

        /// <summary>
        /// Get IWorker<TIn, TOut> which converts TIn type to TOut type by provided input & output types
        /// </summary>
        IWorker<TIn, TOut> Get<TIn, TOut>();

        /// <summary>
        /// Get IWorker from which execution started. It's input/output types indicated in TFirstIn, TFirstOut
        /// </summary>
        IWorker<TFirstIn, TFirstOut> GetFirst();

        /// <summary>
        /// Get IWorker by its input TIn type. It is possible only if single IWorker per TIn registered. Otherwise ArgumentException throwed
        /// </summary>
        dynamic GetIfSingleImplementation<TIn>();
    }
}
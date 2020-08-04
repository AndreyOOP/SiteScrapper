using System;
using System.Collections.Generic;

namespace ParserCore
{
    public interface IWorkersContainer
    {
        /// <summary>
        /// Builds workers execution sequence
        /// </summary>
        /// <param name="workers">Unordered list of workers</param>
        void Register(IEnumerable<object> workers);

        /// <summary>
        /// List of last worker keys where parsing finished
        /// </summary>
        IEnumerable<IInOutKey> Last { get; }

        /// <summary>
        /// Get list of Workers<TIn, TOut> with their key where <param name="typeIn"> is equal to TIn
        /// </summary>
        /// <param name="typeIn">Input model type</param>
        IEnumerable<KeyToWorker> Get(Type typeIn);
    }
}

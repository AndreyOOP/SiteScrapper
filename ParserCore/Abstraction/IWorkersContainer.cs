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
        /// Get list of Workers<TIn, TOut> by their TIn type
        /// </summary>
        /// <param name="typeIn">Input model type euals to Workers TIn</param>
        IEnumerable<object> GetWorkers(Type typeIn);
    }
}

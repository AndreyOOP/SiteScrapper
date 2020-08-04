using System;

namespace ParserCore
{
    /// <summary>
    /// Key identifies Worker<TIn, TOut>
    /// Each worker receives Output model based on Input model, so worker unique defined by Input & Output types combination
    /// </summary>
    public interface IInOutKey
    {
        /// <summary>
        /// Type of worker input model
        /// </summary>
        Type InType { get; }

        /// <summary>
        /// Type of worker output model
        /// </summary>
        Type OutType { get; }
    }
}

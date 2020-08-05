namespace ParserCore
{
    /// <summary>
    /// Worker get TOut model based on TIn model. As well it decides if it is possible to do so, in ToExecute method
    /// </summary>
    public interface IWorker<TIn, TOut>
    {
        /// <summary>
        /// Get TOut model based on TIn model
        /// </summary>
        TOut Parse(TIn model);

        /// <summary>
        /// Check if it is possible (is it has) to execute convertion of TIn to TOut models
        /// </summary>
        bool ToExecute(TIn model);
    }
}

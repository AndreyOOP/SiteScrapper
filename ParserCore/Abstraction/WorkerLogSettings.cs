namespace ParserCore.Abstraction
{
    public abstract class WorkerLogSettings
    {
        /// <summary>
        /// Add stack trace to log record
        /// </summary>
        public abstract bool ShowStackTrace { get; }

        /// <summary>
        /// Add exception message to log record
        /// </summary>
        public abstract bool ShowExceptionMessage { get; }

        /// <summary>
        /// Add TIn model to log
        /// </summary>
        public abstract bool AddInModel { get; }

        /// <summary>
        /// Logs data related to <see cref="IWorker{TIn, TOut}.Parse(TIn)"> method execution
        /// </summary>
        public abstract bool LogOnlyParseMethod { get; }
    }
}

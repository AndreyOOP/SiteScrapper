using System;

namespace ParserCore
{
    public class WorkerLogRecord
    {
        public string WorkerName { get; set; }

        /// <summary>
        /// <see cref="IWorker{TIn, TOut}.IsExecutable(TIn)"/> or <see cref="IWorker{TIn, TOut}.Parse(TIn)"/>
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Record creation datetime
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// TIn model
        /// </summary>
        public object InModel { get; set; }

        public string ExceptionMessage { get; set; }

        public string StackTrace { get; set; }
    }
}

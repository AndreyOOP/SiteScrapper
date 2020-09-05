using System.Collections.Generic;

namespace ParserCore.Abstraction
{
    public interface IInMemoryWorkerLogger
    {
        /// <summary>
        /// Log records with exception details
        /// </summary>
        IEnumerable<WorkerLogRecord> ErrorRecords { get; }

        /// <summary>
        /// All log records (success and with exception)
        /// </summary>
        List<WorkerLogRecord> Records { get; }
    }
}

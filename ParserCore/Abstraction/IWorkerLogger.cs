using System.Collections.Generic;

namespace ParserCore.Abstraction
{
    public interface IWorkerLogger
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

    public interface IInMemoryWorkerLogger : ILogger<WorkerLogRecord>, IWorkerLogger 
    { 
    }
}

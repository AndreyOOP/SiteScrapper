using ParserCore.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace ParserCore
{
    /// <summary>
    /// Required to store single execution of parsers log, in case of exception it will be deserialized to output
    /// </summary>
    public class InMemoryWorkerLogger : IInMemoryWorkerLogger
    {
        public List<WorkerLogRecord> Records { get; private set; } = new List<WorkerLogRecord>();

        public IEnumerable<WorkerLogRecord> ErrorRecords => Records.Where(r => r.ExceptionMessage != null || r.StackTrace != null);

        public void Log(WorkerLogRecord record)
        {
            Records.Add(record);
        }
    }
}

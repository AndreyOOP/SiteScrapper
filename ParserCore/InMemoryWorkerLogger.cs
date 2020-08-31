using ParserCore.Abstraction;
using System.Collections.Generic;

namespace ParserCore
{
    /// <summary>
    /// Required to store single execution of parsers log, in case of exception it will be deserialized to output
    /// </summary>
    public class InMemoryWorkerLogger : ILogger<WorkerLogRecord>
    {
        public List<WorkerLogRecord> records { get; private set; } = new List<WorkerLogRecord>();

        public void Log(WorkerLogRecord record)
        {
            records.Add(record);
        }
    }
}

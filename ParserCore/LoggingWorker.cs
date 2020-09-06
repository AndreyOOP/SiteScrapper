using ParserCore.Abstraction;
using System;

namespace ParserCore
{
    /// <summary>
    /// Log decorator of <see cref="IWorker{TIn, TOut}"/>, adds log records to <see cref="ILogger{TRecord}"/> based on <see cref="WorkerLogSettings"/>
    /// </summary>
    public class LoggingWorker<TIn, TOut> : IWorker<TIn, TOut>
    {
        private WorkerLogSettings settings;
        private ILogger<WorkerLogRecord> logger;
        private IWorker<TIn, TOut> worker;

        public LoggingWorker(IWorker<TIn, TOut> worker, IInMemoryWorkerLogger logger, WorkerLogSettings settings)
        {
            this.settings = settings;
            this.logger = logger;
            this.worker = worker;
        }

        public bool IsExecutable(TIn model)
        {
            if (settings.LogOnlyParseMethod)
                return worker.IsExecutable(model);

            var logRecord = AddNormalFlowFields(nameof(IsExecutable), model);

            try
            {
                logger.Log(logRecord);
                return worker.IsExecutable(model);
            }
            catch (Exception ex)
            {
                AddExceptionFlowFields(logRecord, ex);
                logger.Log(logRecord);
                throw;
            }
        }

        public TOut Parse(TIn model)
        {
            var logRecord = AddNormalFlowFields(nameof(Parse), model);

            try
            {
                logger.Log(logRecord);
                return worker.Parse(model);
            }
            catch (Exception ex)
            {
                AddExceptionFlowFields(logRecord, ex);
                logger.Log(logRecord);
                throw;
            }
        }

        private WorkerLogRecord AddNormalFlowFields(string methodName, object model)
            => new WorkerLogRecord
            {
                WorkerName = worker.GetType().Name,
                MethodName = methodName,
                Created = DateTime.UtcNow,
                InModel = settings.AddInModel ? model : null
            };

        private void AddExceptionFlowFields(WorkerLogRecord logRecord, Exception ex)
        {
            logRecord.ExceptionMessage = settings.ShowExceptionMessage ? ex.Message : null;
            logRecord.StackTrace = settings.ShowStackTrace ? ex.StackTrace : null;
        }
    }
}

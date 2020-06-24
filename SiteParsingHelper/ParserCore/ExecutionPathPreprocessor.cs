﻿using ParserCoreProject.Abstraction;
using System;

namespace ParserCoreProject.ParserCore
{
    internal class ExecutionPathPreprocessor : IWorkerPreprocessor
    {
        private ExecutionPath executionPath;
        
        public string ExecutionPath { get => executionPath.ToString(); }

        public ExecutionPathPreprocessor(ExecutionPath executionPath)
        {
            this.executionPath = executionPath;
        }

        public void Execute(object worker)
        {
            var workerName = worker.GetType().Name;
            
            if (executionPath.AlreadyExecuted(workerName))
                throw new ArgumentException(string.Format(Resource.CycleReference, workerName));

            executionPath.Add(workerName);
        }
    }
}
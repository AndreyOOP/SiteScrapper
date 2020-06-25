using System;

namespace ParserCoreProject.Exceptions
{
    public class WorkUnitException : Exception
    {
        /// <summary>
        /// Information about chain of executed Workers
        /// </summary>
        public string ExecutionPath { get; set; }

        public Exception Exception { get; set; }

        public WorkUnitException()
        {
        }

        public WorkUnitException(string message) : base(message)
        {
        }

        public WorkUnitException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

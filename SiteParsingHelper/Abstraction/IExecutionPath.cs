namespace SiteParsingHelper.Event.Abstraction
{
    internal interface IExecutionPath
    {
        /// <summary>
        /// Adds to execution path name of executed work unit
        /// </summary>
        void Add(string workUnitName);

        /// <summary>
        /// Returns true if workUnit already in the path - already has been executed
        /// </summary>
        /// <param name="workUnitName"></param>
        /// <returns></returns>
        bool AlreadyExecuted(string workUnitName);

        /// <summary>
        /// Displays already executed path
        /// </summary>
        string ToString();
    }
}
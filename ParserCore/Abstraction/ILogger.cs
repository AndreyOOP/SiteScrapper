namespace ParserCore.Abstraction
{
    public interface ILogger<TRecord>
    {
        /// <summary>
        /// Logs TRecord data
        /// </summary>
        /// <param name="record">Log message, could be more complex than just a string</param>
        void Log(TRecord record);
    }
}

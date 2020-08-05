namespace ParserCore
{
    public interface IParser<TIn, TParsingResult>
    {
        /// <summary>
        /// Gather information based on provided input
        /// </summary>
        /// <param name="model">Input parameters e.g. part Id any input parameters to identify what to parse</param>
        /// <returns>Final result of data parsing</returns>
        TParsingResult Parse(TIn model);
    }
}
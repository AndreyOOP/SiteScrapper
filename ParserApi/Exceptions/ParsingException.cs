using System;

namespace ParserApi.Exceptions
{
    // todo: documentation
    public class ParsingException : Exception
    {
        public ParsingException()
        {
        }

        public ParsingException(string message) : base(message)
        {
        }
    }
}
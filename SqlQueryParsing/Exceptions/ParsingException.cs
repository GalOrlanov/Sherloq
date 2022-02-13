using System;

namespace SqlQueryParsing.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string message)
            : base(message)
        {
        }
    }
}

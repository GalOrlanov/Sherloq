using System;

namespace SqlQueryParsing.Exceptions
{
    public class NoQueryExecutionIdException : Exception
    {
        public NoQueryExecutionIdException(string message)
            : base(message)
        {
        }
    }
}
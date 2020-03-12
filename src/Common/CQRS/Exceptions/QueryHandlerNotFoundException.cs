using System;

namespace AGTec.Common.CQRS.Exceptions
{
    public class QueryHandlerNotFoundException : Exception
    {
        public QueryHandlerNotFoundException() : base()
        { }

        public QueryHandlerNotFoundException(string message) : base(message)
        { }
    }
}

using System;

namespace AGTec.Common.CQRS.Exceptions
{
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException() : base()
        { }

        public CommandHandlerNotFoundException(string message) : base(message)
        { }
    }
}

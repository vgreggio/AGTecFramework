using System;

namespace AGTec.Common.CQRS.Exceptions
{
    public class EventHandlerNotFoundException : Exception
    {
        public EventHandlerNotFoundException() : base()
        { }

        public EventHandlerNotFoundException(string message) : base(message)
        { }
    }
}

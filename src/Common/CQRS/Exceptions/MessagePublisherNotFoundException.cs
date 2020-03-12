using System;

namespace AGTec.Common.CQRS.Exceptions
{
    public class MessagePublisherNotFoundException : Exception
    {
        public MessagePublisherNotFoundException() : base()
        { }

        public MessagePublisherNotFoundException(string message) : base(message)
        { }
    }
}

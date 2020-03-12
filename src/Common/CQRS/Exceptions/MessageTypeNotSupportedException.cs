using System;

namespace AGTec.Common.CQRS.Exceptions
{
    public class MessageTypeNotSupportedException : Exception
    {
        public MessageTypeNotSupportedException() : base()
        { }

        public MessageTypeNotSupportedException(string message) : base(message)
        { }
    }
}

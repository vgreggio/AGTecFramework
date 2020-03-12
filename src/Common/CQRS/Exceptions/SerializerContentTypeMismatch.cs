using System;

namespace AGTec.Common.CQRS.Exceptions
{
    public class SerializerContentTypeMismatch : Exception
    {
        public SerializerContentTypeMismatch() : base()
        { }

        public SerializerContentTypeMismatch(string message) : base(message)
        { }
    }
}

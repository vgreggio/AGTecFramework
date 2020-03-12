using System;

namespace AGTec.Common.CQRS.Exceptions
{
    public class PayloadSerializerNotFound : Exception
    {
        public PayloadSerializerNotFound() : base()
        { }

        public PayloadSerializerNotFound(string message) : base(message)
        { }
    }
}

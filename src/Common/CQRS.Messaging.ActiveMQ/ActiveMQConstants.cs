using System;
using System.Collections.Generic;
using System.Text;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    static class ActiveMQConstants
    {
        public static class Message
        {
            public static class Properties
            {
                public const String ContentType = "Content-type";
                public const String Label = "Label";
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    public interface IActiveMQMessageFilterFactory
    {
        String Create(IEnumerable<IMessageFilter> filters);
    }
}

using System;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    public interface IActiveMQMessageFilterFactory
    {
        String Create(IMessageFilter filter);
    }
}

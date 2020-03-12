using System.Collections.Generic;

namespace AGTec.Common.CQRS.Messaging
{
    public interface IMessageHandler
    {
        void Handle(string topicName, string subscriptionName, IEnumerable<IMessageFilter> filters = null);
    }
}

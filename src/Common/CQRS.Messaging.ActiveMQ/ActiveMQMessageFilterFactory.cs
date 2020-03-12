using AGTec.Common.CQRS.Messaging.Exceptions;
using System;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    class ActiveMQMessageFilterFactory : IActiveMQMessageFilterFactory
    {
        public String Create(IMessageFilter filter)
        {
            switch (filter.Type)
            {
                case MessageFilterType.CorrelationIdFilter:
                    return "NMSCorrelationID =" + filter.Expression;

                case MessageFilterType.LabelFilter:
                    return ActiveMQConstants.Message.Properties.Label + "=" + filter.Expression;

                case MessageFilterType.QueryFilter:
                    return filter.Name + "=" + filter.Expression;

                default:
                    throw new InvalidMessageFilterException($"Filter {filter.Type} is invalid for {this.GetType().AssemblyQualifiedName} implementation.");
            }
        }
    }
}

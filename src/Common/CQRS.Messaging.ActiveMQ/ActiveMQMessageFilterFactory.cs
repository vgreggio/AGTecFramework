using AGTec.Common.Base.Extensions;
using AGTec.Common.CQRS.Messaging.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    class ActiveMQMessageFilterFactory : IActiveMQMessageFilterFactory
    {
        public string Create(IEnumerable<IMessageFilter> filters)
        {
            if (filters == null || filters.Any() == false)
                return null;

            var result = String.Empty;
            filters.ForEach(filter =>
            {
                if (String.IsNullOrWhiteSpace(result) == false)
                    result += " AND ";

                switch (filter.Type)
                {
                    case MessageFilterType.CorrelationIdFilter:
                        result += "NMSCorrelationID = " + filter.Expression;
                        break;

                    case MessageFilterType.LabelFilter:
                        result += "Label = " + filter.Expression;
                        break;

                    case MessageFilterType.QueryFilter:
                        result += filter.Expression;
                        break;

                    default:
                        throw new InvalidMessageFilterException($"Filter {filter.Type} is invalid for {this.GetType().AssemblyQualifiedName} implementation.");
                }
            });

            return result;
        }
    }
}

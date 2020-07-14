using System;
using System.Collections.Generic;
using System.Text;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    public class ActiveMQMessageBusConfiguration : IMessageBusConfiguration
    {
        public const string ConfigSectionName = "ActiveMQ";

        public string ConnectionString { get; set; }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(ConnectionString) == false;
        }
    }
}
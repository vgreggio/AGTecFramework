using System;

namespace AGTec.Common.CQRS.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PublishableAttribute : Attribute
    {
        public PublishableAttribute(string label, string version, string topicName)
        {
            this.Label = label;
            this.Version = version;
            this.TopicName = topicName;
        }

        public string Label { get; }
        public string Version { get; }
        public string TopicName { get; }
    }
}

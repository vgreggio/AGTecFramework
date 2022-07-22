using System;

namespace AGTec.Common.Document.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SchemaVersionAttribute : Attribute
    {
        public SchemaVersionAttribute(int version)
        {
            Version = version;
        }

        public int Version { get; }
    }
}

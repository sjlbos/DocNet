
using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    [Serializable]
    public class ConfigurationException : Exception
    {
        public DocNetStatus Status { get; private set; }

        public ConfigurationException(string message, DocNetStatus status) : base(message) { Status = status; }

        public ConfigurationException(string message, DocNetStatus status, Exception innerException) : base(message, innerException) { Status = status; }

        protected ConfigurationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

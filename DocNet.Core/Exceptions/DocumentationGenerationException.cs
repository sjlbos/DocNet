using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    [Serializable]
    public class DocumentationGenerationException : Exception
    {
        public DocumentationGenerationException() { }

        public DocumentationGenerationException(string message) : base(message) { }

        public DocumentationGenerationException(string message, Exception innerException) : base(message, innerException) { }

        protected DocumentationGenerationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

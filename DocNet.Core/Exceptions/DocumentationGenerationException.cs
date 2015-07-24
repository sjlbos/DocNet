using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    /// <summary>
    /// An exception to be thrown when the program is unable to generate documentation for a particular model element.
    /// </summary>
    [Serializable]
    public class DocumentationGenerationException : Exception
    {
        public DocumentationGenerationException() { }

        public DocumentationGenerationException(string message) : base(message) { }

        public DocumentationGenerationException(string message, Exception innerException) : base(message, innerException) { }

        protected DocumentationGenerationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    /// <summary>
    /// An exception to be thrown when an INestableElement is added to a parent element that does not support the child's type.
    /// </summary>
    [Serializable]
    public class IllegalChildElementException : CsParsingException
    {
        public IllegalChildElementException() { }

        public IllegalChildElementException(string message) : base(message) { }

        public IllegalChildElementException(string message, Exception innerException) : base(message, innerException) { }

        protected IllegalChildElementException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

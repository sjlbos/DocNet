using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    [Serializable]
    public class IllegalChildElementException : CsParsingException
    {
        public IllegalChildElementException() { }

        public IllegalChildElementException(string message) : base(message) { }

        public IllegalChildElementException(string message, Exception innerException) : base(message, innerException) { }

        protected IllegalChildElementException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

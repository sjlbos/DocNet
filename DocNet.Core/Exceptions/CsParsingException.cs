using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    [Serializable]
    public class CsParsingException : Exception
    {
        public CsParsingException() { }

        public CsParsingException(string message) : base(message) { }

        public CsParsingException(string message, Exception innerException) : base(message, innerException) { }

        protected CsParsingException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

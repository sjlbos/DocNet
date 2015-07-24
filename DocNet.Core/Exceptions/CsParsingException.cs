using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    /// <summary>
    /// An exception to be thrown when the program encounters an unrecoverable error while parsing code.
    /// </summary>
    [Serializable]
    public class CsParsingException : Exception
    {
        public CsParsingException() { }

        public CsParsingException(string message) : base(message) { }

        public CsParsingException(string message, Exception innerException) : base(message, innerException) { }

        protected CsParsingException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

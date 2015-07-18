using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    [Serializable]
    public class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException() { }

        public InvalidFileTypeException(string message) : base(message) { }

        public InvalidFileTypeException(string message, Exception innerException) : base(message, innerException) { }

        protected InvalidFileTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

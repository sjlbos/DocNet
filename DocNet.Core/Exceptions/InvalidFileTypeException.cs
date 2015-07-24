using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    /// <summary>
    /// An excetption to be thrown when the program tries to parse a file format it does not understand.
    /// </summary>
    [Serializable]
    public class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException() { }

        public InvalidFileTypeException(string message) : base(message) { }

        public InvalidFileTypeException(string message, Exception innerException) : base(message, innerException) { }

        protected InvalidFileTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

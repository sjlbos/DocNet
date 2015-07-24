using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    /// <summary>
    /// An exception to be thrown when the program tries to add a child element to a parent element that already contains an element with the same name.
    /// </summary>
    [Serializable]
    public class NamingCollisionException : CsParsingException
    {
        public NamingCollisionException() { }

        public NamingCollisionException(string message) : base(message) { }

        public NamingCollisionException(string message, Exception innerException) : base(message, innerException) { }

        protected NamingCollisionException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

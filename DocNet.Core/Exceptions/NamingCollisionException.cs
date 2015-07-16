using System;
using System.Runtime.Serialization;

namespace DocNet.Core.Exceptions
{
    [Serializable]
    public class NamingCollisionException : CsParsingException
    {
        public NamingCollisionException() { }

        public NamingCollisionException(string message) : base(message) { }

        public NamingCollisionException(string message, Exception innerException) : base(message, innerException) { }

        protected NamingCollisionException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}

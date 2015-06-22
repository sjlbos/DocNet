using System;

namespace DocNet.Core.Exceptions
{
    public class CsParsingException : Exception
    {
        public CsParsingException() { }

        public CsParsingException(string message) : base(message) { }

        public CsParsingException(string message, Exception innerException) : base(message, innerException) { }
    }
}

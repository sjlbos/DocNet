
using System;

namespace DocNet.Core.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"
        ), Serializable]
    public class ConfigurationException : Exception
    {
        public DocNetStatus Status { get; private set; }

        public ConfigurationException(string message, DocNetStatus status) : base(message)
        {
            Status = status;
        }

        public ConfigurationException(string message, DocNetStatus status, Exception innerException)
            : base(message, innerException)
        {
            Status = status;
        }
    }
}

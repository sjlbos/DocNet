
using System;

namespace DocNet.Core.Exceptions
{
    /// <summary>
    /// An exception to be thrown when the DocNet controller is supplied with incorrect or missing configuration properties.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"
        ), Serializable]
    public class ConfigurationException : Exception
    {
        /// <summary>
        /// The DocNet status code representing the configuration error.
        /// </summary>
        public DocNetStatus Status { get; private set; }

        /// <param name="message">The message that will be displayed to the user.</param>
        /// <param name="status">The status code representing the configuration error.</param>
        public ConfigurationException(string message, DocNetStatus status) : base(message)
        {
            Status = status;
        }

        /// <param name="message">The message that will be displayed to the user.</param>
        /// <param name="status">The status code representing the configuration error.</param>
        /// <param name="innerException">The exception that resulted in the configuration exception being thrown.</param>
        public ConfigurationException(string message, DocNetStatus status, Exception innerException)
            : base(message, innerException)
        {
            Status = status;
        }
    }
}

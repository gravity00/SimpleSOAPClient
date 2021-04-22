using System;

namespace SimpleSOAPClient.Exceptions
{
    /// <summary>
    /// Thrown when a problem is encountered deserializing MTOM attachments.
    /// </summary>
    public class SoapAttachmentDeserializationException : SoapClientException
    {
        /// <inheritdoc />
        public SoapAttachmentDeserializationException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public SoapAttachmentDeserializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
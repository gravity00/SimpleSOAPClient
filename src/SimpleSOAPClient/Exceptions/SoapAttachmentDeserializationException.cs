using System;

namespace SimpleSOAPClient.Exceptions
{
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
using System;

namespace SimpleSOAPClient
{
    /// <summary>
    /// Represents a SOAP envelope for requests
    /// </summary>
    public class SoapEnvelopeRequest : SoapEnvelope<SoapHeader, object>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="content">The body content</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SoapEnvelopeRequest(object content) : base(content)
        {

        }
    }
}
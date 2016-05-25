namespace SimpleSOAPClient.Handlers
{
    using System;
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope response data.
    /// </summary>
    public class ResponseEnvelopeHandlerData : HandlerData, IResponseEnvelopeHandlerData
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ResponseEnvelopeHandlerData(string url, string action) 
            : base(url, action)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <param name="envelope">The SOAP envelope</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ResponseEnvelopeHandlerData(string url, string action, SoapEnvelope envelope) 
            : base(url, action)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            Envelope = envelope;
        }

        /// <summary>
        /// The SOAP Envelope response
        /// </summary>
        public SoapEnvelope Envelope { get; set; }
    }
}
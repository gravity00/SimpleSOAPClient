namespace SimpleSOAPClient.Handlers
{
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope request data.
    /// </summary>
    public class RequestEnvelopeHandlerData : HandlerData, IRequestEnvelopeHandlerData
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public RequestEnvelopeHandlerData(string url, string action) 
            : base(url, action)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <param name="envelope">The SOAP envelope</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public RequestEnvelopeHandlerData(string url, string action, SoapEnvelope envelope) 
            : base(url, action)
        {
            Envelope = envelope;
        }

        /// <summary>
        /// The SOAP Envelope to be serialized
        /// </summary>
        public SoapEnvelope Envelope { get; set; }
    }
}
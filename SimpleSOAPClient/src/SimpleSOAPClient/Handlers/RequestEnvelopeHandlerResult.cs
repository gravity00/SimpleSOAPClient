namespace SimpleSOAPClient.Handlers
{
    using System;
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope request handler result.
    /// </summary>
    public class RequestEnvelopeHandlerResult : HandlerResult, IRequestEnvelopeHandlerResult
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="cancelHandlerFlow">Cancel the current handler flow?</param>
        /// <param name="envelope">The SOAP envelope</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RequestEnvelopeHandlerResult(bool cancelHandlerFlow, SoapEnvelope envelope) 
            : base(cancelHandlerFlow)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            Envelope = envelope;
        }

        /// <summary>
        /// The resultant SOAP Envelope that will be serialized
        /// </summary>
        public SoapEnvelope Envelope { get; }
    }
}
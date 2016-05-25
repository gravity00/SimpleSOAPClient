namespace SimpleSOAPClient.Handlers
{
    using System;
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope response handler result.
    /// </summary>
    public class ResponseEnvelopeHandlerResult : HandlerResult, IResponseEnvelopeHandlerResult
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="cancelHandlerFlow">Cancel the current handler flow?</param>
        /// <param name="envelope">The resultant SOAP Envelope response</param>
        public ResponseEnvelopeHandlerResult(bool cancelHandlerFlow, SoapEnvelope envelope) 
            : base(cancelHandlerFlow)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            Envelope = envelope;
        }

        /// <summary>
        /// The resultant SOAP Envelope response
        /// </summary>
        public SoapEnvelope Envelope { get; }
    }
}
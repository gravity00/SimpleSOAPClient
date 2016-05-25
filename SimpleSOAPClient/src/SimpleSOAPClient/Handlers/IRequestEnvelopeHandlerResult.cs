namespace SimpleSOAPClient.Handlers
{
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope request handler result.
    /// </summary>
    public interface IRequestEnvelopeHandlerResult : IHandlerResult
    {
        /// <summary>
        /// The resultant SOAP Envelope that will be serialized
        /// </summary>
        SoapEnvelope Envelope { get; }
    }
}
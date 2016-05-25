namespace SimpleSOAPClient.Handlers
{
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope response handler result.
    /// </summary>
    public interface IResponseEnvelopeHandlerResult : IHandlerResult
    {
        /// <summary>
        /// The resultant SOAP Envelope response
        /// </summary>
        SoapEnvelope Envelope { get; }
    }
}
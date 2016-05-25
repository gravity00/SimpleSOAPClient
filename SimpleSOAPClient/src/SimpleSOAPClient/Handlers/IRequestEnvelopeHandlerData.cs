namespace SimpleSOAPClient.Handlers
{
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope request handler data.
    /// </summary>
    public interface IRequestEnvelopeHandlerData : IHandlerData
    {
        /// <summary>
        /// The current SOAP Envelope to be serialized
        /// </summary>
        SoapEnvelope Envelope { get; }
    }
}
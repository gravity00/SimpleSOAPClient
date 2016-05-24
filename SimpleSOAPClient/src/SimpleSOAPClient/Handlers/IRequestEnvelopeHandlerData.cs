namespace SimpleSOAPClient.Handlers
{
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope request data.
    /// </summary>
    public interface IRequestEnvelopeHandlerData : IHandlerData
    {
        /// <summary>
        /// The SOAP Envelope to be serialized
        /// </summary>
        SoapEnvelope Envelope { get; set; }
    }
}
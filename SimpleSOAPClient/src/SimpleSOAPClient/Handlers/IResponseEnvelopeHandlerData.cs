namespace SimpleSOAPClient.Handlers
{
    using Models;

    /// <summary>
    /// Represents the SOAP Envelope response data.
    /// </summary>
    public interface IResponseEnvelopeHandlerData : IHandlerData
    {
        /// <summary>
        /// The SOAP Envelope response
        /// </summary>
        SoapEnvelope Envelope { get; set; }
    }
}
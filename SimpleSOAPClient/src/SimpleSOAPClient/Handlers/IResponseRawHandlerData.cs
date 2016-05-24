namespace SimpleSOAPClient.Handlers
{
    using System.Net.Http;

    /// <summary>
    /// Represents the raw response data.
    /// </summary>
    public interface IResponseRawHandlerData : IHandlerData
    {
        /// <summary>
        /// The HTTP response message
        /// </summary>
        HttpResponseMessage Response { get; set; }

        /// <summary>
        /// The string content that will be deserialized as a SOAP Envelope
        /// </summary>
        string Content { get; set; }
    }
}
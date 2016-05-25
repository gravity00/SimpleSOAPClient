namespace SimpleSOAPClient.Handlers
{
    using System.Net.Http;

    /// <summary>
    /// Represents the raw response handler result.
    /// </summary>
    public interface IResponseRawHandlerResult : IHandlerResult
    {
        /// <summary>
        /// The resultant HTTP response message
        /// </summary>
        HttpResponseMessage Response { get; }

        /// <summary>
        /// The resultant string content that will be deserialized as a SOAP Envelope
        /// </summary>
        string Content { get; }
    }
}
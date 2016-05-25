namespace SimpleSOAPClient.Handlers
{
    using System.Net.Http;

    /// <summary>
    /// Represents the raw request handler result.
    /// </summary>
    public interface IRequestRawHandlerResult : IHandlerResult
    {
        /// <summary>
        /// The resultant HTTP request message
        /// </summary>
        HttpRequestMessage Request { get; }

        /// <summary>
        /// The resultant string content that will be included in the <see cref="Request"/>
        /// </summary>
        string Content { get; }
    }
}
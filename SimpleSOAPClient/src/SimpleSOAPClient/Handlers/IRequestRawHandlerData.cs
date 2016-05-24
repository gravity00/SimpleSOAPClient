namespace SimpleSOAPClient.Handlers
{
    using System.Net.Http;

    /// <summary>
    /// Represents the raw request data.
    /// </summary>
    public interface IRequestRawHandlerData : IHandlerData
    {
        /// <summary>
        /// The HTTP request message
        /// </summary>
        HttpRequestMessage Request { get; set; }

        /// <summary>
        /// The string content that will be included in the <see cref="Request"/>
        /// </summary>
        string Content { get; set; }
    }
}
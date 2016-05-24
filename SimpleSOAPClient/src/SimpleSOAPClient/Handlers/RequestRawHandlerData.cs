namespace SimpleSOAPClient.Handlers
{
    using System.Net.Http;

    /// <summary>
    /// Represents the raw request data.
    /// </summary>
    public class RequestRawHandlerData : HandlerData, IRequestRawHandlerData
    {
        /// <summary>
        /// Creates new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public RequestRawHandlerData(string url, string action) 
            : base(url, action)
        {

        }

        /// <summary>
        /// Creates new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <param name="request">The HTTP request message</param>
        /// <param name="content">The request content</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public RequestRawHandlerData(string url, string action, HttpRequestMessage request, string content) 
            : base(url, action)
        {
            Request = request;
            Content = content;
        }

        /// <summary>
        /// The HTTP request message
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// The string content that will be included in the <see cref="IRequestRawHandlerData.Request"/>
        /// </summary>
        public string Content { get; set; }
    }
}
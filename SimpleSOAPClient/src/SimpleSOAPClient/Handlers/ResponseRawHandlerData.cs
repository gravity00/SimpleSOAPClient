namespace SimpleSOAPClient.Handlers
{
    using System.Net.Http;

    /// <summary>
    /// Represents the raw response data.
    /// </summary>
    public class ResponseRawHandlerData : HandlerData, IResponseRawHandlerData
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public ResponseRawHandlerData(string url, string action) 
            : base(url, action)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <param name="response">The HTTP response</param>
        /// <param name="content">The response content</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public ResponseRawHandlerData(string url, string action, HttpResponseMessage response, string content) 
            : base(url, action)
        {
            Response = response;
            Content = content;
        }

        /// <summary>
        /// The HTTP response message
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// The string content that will be deserialized as a SOAP Envelope
        /// </summary>
        public string Content { get; set; }
    }
}
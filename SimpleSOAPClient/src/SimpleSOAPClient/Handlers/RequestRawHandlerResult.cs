namespace SimpleSOAPClient.Handlers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Represents the raw request handler result.
    /// </summary>
    public class RequestRawHandlerResult : HandlerResult, IRequestRawHandlerResult
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="cancelHandlerFlow">Cancel the current handler flow?</param>
        /// <param name="request">The resultant HTTP request message</param>
        /// <param name="content">The resultant string content</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RequestRawHandlerResult(bool cancelHandlerFlow, HttpRequestMessage request, string content) 
            : base(cancelHandlerFlow)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (content == null) throw new ArgumentNullException(nameof(content));

            Request = request;
            Content = content;
        }

        /// <summary>
        /// The resultant HTTP request message
        /// </summary>
        public HttpRequestMessage Request { get; }

        /// <summary>
        /// The resultant string content that will be included in the <see cref="IRequestRawHandlerResult.Request"/>
        /// </summary>
        public string Content { get; }
    }
}
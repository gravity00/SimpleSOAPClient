namespace SimpleSOAPClient.Handlers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Represents the raw response handler result.
    /// </summary>
    public class ResponseRawHandlerResult : HandlerResult, IResponseRawHandlerResult
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="cancelHandlerFlow">Cancel the current handler flow?</param>
        /// <param name="response">The resultant HTTP response message</param>
        /// <param name="content">The resultant string content that will be deserialized as a SOAP Envelope</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ResponseRawHandlerResult(bool cancelHandlerFlow, HttpResponseMessage response, string content) 
            : base(cancelHandlerFlow)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (content == null) throw new ArgumentNullException(nameof(content));

            Response = response;
            Content = content;
        }

        /// <summary>
        /// The resultant HTTP response message
        /// </summary>
        public HttpResponseMessage Response { get; }

        /// <summary>
        /// The resultant string content that will be deserialized as a SOAP Envelope
        /// </summary>
        public string Content { get; }
    }
}
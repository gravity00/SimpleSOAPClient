#region License
// // The MIT License (MIT)
// // 
// // Copyright (c) 2016 João Simões
// // 
// // Permission is hereby granted, free of charge, to any person obtaining a copy
// // of this software and associated documentation files (the "Software"), to deal
// // in the Software without restriction, including without limitation the rights
// // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// // copies of the Software, and to permit persons to whom the Software is
// // furnished to do so, subject to the following conditions:
// // 
// // The above copyright notice and this permission notice shall be included in all
// // copies or substantial portions of the Software.
// // 
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// // SOFTWARE.
#endregion
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
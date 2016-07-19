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
    /// Represents the raw request data.
    /// </summary>
    public class RequestRawHandlerData : HandlerData, IRequestRawHandlerData
    {
        /// <summary>
        /// Creates new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <exception cref="ArgumentNullException"></exception>
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
        /// <exception cref="ArgumentNullException"></exception>
        public RequestRawHandlerData(string url, string action, HttpRequestMessage request, string content) 
            : base(url, action)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (content == null) throw new ArgumentNullException(nameof(content));

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
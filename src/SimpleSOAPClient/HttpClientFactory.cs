#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimpleSOAPClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    /// <summary>
    /// The <see cref="HttpClient"/> factory to be used by <see cref="SoapClient"/>
    /// when no client is provided.
    /// </summary>
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly List<KeyValuePair<string, IEnumerable<string>>> _defaultRequestHeaders;

        /// <summary>
        /// Gets or sets the default base address of Uniform Resource Identifier (URI) of the Internet 
        /// resource used when sending requests.
        /// </summary>
        public Uri DefaultBaseAddress { get; set; }

        /// <summary>
        /// The default response content buffer size
        /// </summary>
        public long DefaultMaxResponseContentBufferSize { get; set; }

        /// <summary>
        /// The default request timeout
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; }

        /// <summary>
        /// The defauld HTTP request headers that should be sent in every request.
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<string, IEnumerable<string>>> DefaultRequestHeaders => _defaultRequestHeaders;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public HttpClientFactory()
        {
            var httpClient = new HttpClient();
            DefaultBaseAddress = httpClient.BaseAddress;
            DefaultMaxResponseContentBufferSize = httpClient.MaxResponseContentBufferSize;
            DefaultTimeout = httpClient.Timeout;

            _defaultRequestHeaders =
                httpClient.DefaultRequestHeaders.Select(
                    e => new KeyValuePair<string, IEnumerable<string>>(e.Key, e.Value)).ToList();
        }

        /// <summary>
        /// Adds the given name avd values to the default request header collection.
        /// </summary>
        /// <param name="name">The header name</param>
        /// <param name="values">The header values</param>
        public void AddDefaultRequestHeader(string name, IEnumerable<string> values)
        {
            _defaultRequestHeaders.Add(new KeyValuePair<string, IEnumerable<string>>(name, values));
        }

        /// <summary>
        /// Adds the given name avd values to the default request header collection.
        /// </summary>
        /// <param name="name">The header name</param>
        /// <param name="values">The header values</param>
        public void AddDefaultRequestHeader(string name, params string[] values)
        {
            _defaultRequestHeaders.Add(new KeyValuePair<string, IEnumerable<string>>(name, values));
        }

        #region Implementation of IHttpClientFactory

        /// <summary>
        /// Returns a new <see cref="HttpClient"/> instance.
        /// </summary>
        /// <returns>The HTTP client</returns>
        public HttpClient Get()
        {
            var client = new HttpClient
            {
                BaseAddress = DefaultBaseAddress,
                MaxResponseContentBufferSize = DefaultMaxResponseContentBufferSize,
                Timeout = DefaultTimeout
            };
            foreach (var header in DefaultRequestHeaders)
                client.DefaultRequestHeaders.Add(header.Key, header.Value);

            return client;
        }

        /// <summary>
        /// Returns a new <see cref="HttpClient"/> instance that should used
        /// the given <see cref="HttpMessageHandler"/>.
        /// </summary>
        /// <param name="handler">The HTTP message handler</param>
        /// <returns>The HTTP client</returns>
        public HttpClient Get(HttpMessageHandler handler)
        {
            var client = new HttpClient(handler)
            {
                BaseAddress = DefaultBaseAddress,
                MaxResponseContentBufferSize = DefaultMaxResponseContentBufferSize,
                Timeout = DefaultTimeout
            };
            foreach (var header in DefaultRequestHeaders)
                client.DefaultRequestHeaders.Add(header.Key, header.Value);

            return client;
        }

        #endregion
    }
}
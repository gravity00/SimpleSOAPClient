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
namespace SimpleSOAPClient.Handlers
{
    using System;

    /// <summary>
    /// Base class for SOAP Handler argument classes
    /// </summary>
    public abstract class SoapHandlerArguments
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The SOAP service url</param>
        /// <param name="action">The SOAP action</param>
        /// <param name="trackingId">An optional tracking id. If null <see cref="Guid.NewGuid"/> will be used.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected SoapHandlerArguments(string url, string action, Guid? trackingId = null)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));
            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(action));

            Url = url;
            Action = action;
            TrackingId = trackingId ?? Guid.NewGuid();
        }

        #region Implementation of ISoapHandlerArguments

        /// <summary>
        /// The URL being invoked
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The action being invoked
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// A unique identifier to track the current request
        /// </summary>
        public Guid TrackingId { get; }

        /// <summary>
        /// An object state that will be passed
        /// accross all the handlers
        /// </summary>
        public object State { get; set; }

        #endregion
    }
}
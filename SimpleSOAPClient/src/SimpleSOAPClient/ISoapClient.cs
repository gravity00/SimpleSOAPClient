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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface ISoapClient
    {
        /// <summary>
        /// Handler that can manipulate the <see cref="SoapEnvelope"/>
        /// before serialization.
        /// </summary>
        Func<string, SoapEnvelope, SoapEnvelope> RequestEnvelopeHandler { get; set; }

        /// <summary>
        /// Handler that can manipulate the generated XML string.
        /// </summary>
        Func<string, string, string> RequestRawHandler { get; set; }

        /// <summary>
        /// Handler that can manipulate the <see cref="SoapEnvelope"/> returned
        /// by the SOAP Endpoint.
        /// </summary>
        Func<string, SoapEnvelope, SoapEnvelope> ResponseEnvelopeHandler { get; set; }

        /// <summary>
        /// Handler that can manipulate the returned string before deserialization.
        /// </summary>
        Func<string, HttpResponseMessage, string, string> ResponseRawHandler { get; set; }

        /// <summary>
        /// Sends the given <see cref="SoapEnvelope"/> into the specified url.
        /// </summary>
        /// <param name="url">The url that will receive the request</param>
        /// <param name="requestEnvelope">The <see cref="SoapEnvelope"/> to be sent</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited for the result</returns>
        Task<SoapEnvelope> SendAsync(
            string url, SoapEnvelope requestEnvelope, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Sends the given <see cref="SoapEnvelope"/> into the specified url.
        /// </summary>
        /// <param name="url">The url that will receive the request</param>
        /// <param name="requestEnvelope">The <see cref="SoapEnvelope"/> to be sent</param>
        /// <returns>The resulting <see cref="SoapEnvelope"/></returns>
        SoapEnvelope Send(string url, SoapEnvelope requestEnvelope);
    }
}
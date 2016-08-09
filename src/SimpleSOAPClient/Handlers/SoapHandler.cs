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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Models.V1Dot1;

    /// <summary>
    /// Base SOAP Handler that extending classes can use to override
    /// only specific operations.
    /// </summary>
    public abstract class SoapHandler : ISoapHandler
    {
        #region Implementation of ISoapHandler

        /// <summary>
        /// The order for which the handler will be executed
        /// </summary>
        public int Order { get; set; }

        #region OnSoapEnvelopeRequest

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public virtual void OnSoapEnvelopeV1Dot1Request(ISoapClient client, OnSoapEnvelopeV1Dot1RequestArguments arguments)
        {
            
        }

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public virtual Task OnSoapEnvelopeV1Dot1RequestAsync(ISoapClient client, OnSoapEnvelopeV1Dot1RequestArguments arguments, CancellationToken ct)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public virtual void OnSoapEnvelopeV1Dot2Request(ISoapClient client, OnSoapEnvelopeV1Dot2RequestArguments arguments)
        {

        }

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public virtual Task OnSoapEnvelopeV1Dot2RequestAsync(ISoapClient client, OnSoapEnvelopeV1Dot2RequestArguments arguments, CancellationToken ct)
        {
            return Task.FromResult(true);
        }

        #endregion

        #region OnHttpRequest

        /// <summary>
        /// Method invoked before sending the <see cref="HttpRequestMessage"/> to the server.
        /// Useful to log the request or change properties like HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public virtual void OnHttpRequest(ISoapClient client, OnHttpRequestArguments arguments)
        {

        }

        /// <summary>
        /// Method invoked before sending the <see cref="HttpRequestMessage"/> to the server.
        /// Useful to log the request or change properties like HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public virtual Task OnHttpRequestAsync(ISoapClient client, OnHttpRequestArguments arguments, CancellationToken ct)
        {
            return Task.FromResult(true);
        }

        #endregion

        #region OnHttpResponse

        /// <summary>
        /// Method invoked after receiving a <see cref="HttpResponseMessage"/> from the server.
        /// Useful to log the response or validate HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public virtual void OnHttpResponse(ISoapClient client, OnHttpResponseArguments arguments)
        {

        }

        /// <summary>
        /// Method invoked after receiving a <see cref="HttpResponseMessage"/> from the server.
        /// Useful to log the response or validate HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public virtual Task OnHttpResponseAsync(ISoapClient client, OnHttpResponseArguments arguments, CancellationToken ct)
        {
            return Task.FromResult(true);
        }

        #endregion

        #region OnSoapEnvelopeResponse

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public virtual void OnSoapEnvelopeV1Dot1Response(ISoapClient client, OnSoapEnvelopeV1Dot1ResponseArguments arguments)
        {

        }

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public virtual Task OnSoapEnvelopeV1Dot1ResponseAsync(ISoapClient client, OnSoapEnvelopeV1Dot1ResponseArguments arguments, CancellationToken ct)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public virtual void OnSoapEnvelopeV1Dot2Response(ISoapClient client, OnSoapEnvelopeV1Dot2ResponseArguments arguments)
        {

        }

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public virtual Task OnSoapEnvelopeV1Dot2ResponseAsync(ISoapClient client, OnSoapEnvelopeV1Dot2ResponseArguments arguments, CancellationToken ct)
        {
            return Task.FromResult(true);
        }

        #endregion

        #endregion
    }
}
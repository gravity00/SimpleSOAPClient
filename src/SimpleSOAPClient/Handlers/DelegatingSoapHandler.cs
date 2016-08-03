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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// SOAP Handler that exposes delegates for each handling operation.
    /// </summary>
    public sealed class DelegatingSoapHandler : ISoapHandler
    {
        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnSoapEnvelopeRequest"/> method.
        /// </summary>
        public Action<ISoapClient, OnSoapEnvelopeRequestArguments> OnSoapEnvelopeRequestAction { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnSoapEnvelopeRequestAsync"/> method.
        /// </summary>
        public Func<ISoapClient, OnSoapEnvelopeRequestArguments, CancellationToken, Task> OnSoapEnvelopeRequestAsyncAction { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnHttpRequest"/> method.
        /// </summary>
        public Action<ISoapClient, OnHttpRequestArguments> OnHttpRequestAction { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnHttpRequestAsync"/> method.
        /// </summary>
        public Func<ISoapClient, OnHttpRequestArguments, CancellationToken, Task> OnHttpRequestAsyncAction { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnHttpResponse"/> method.
        /// </summary>
        public Action<ISoapClient, OnHttpResponseArguments> OnHttpResponseAction { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnHttpResponseAsync"/> method.
        /// </summary>
        public Func<ISoapClient, OnHttpResponseArguments, CancellationToken, Task> OnHttpResponseAsyncAction { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnSoapEnvelopeResponse"/> method.
        /// </summary>
        public Action<ISoapClient, OnSoapEnvelopeResponseArguments> OnSoapEnvelopeResponseAction { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.OnSoapEnvelopeResponseAsync"/> method.
        /// </summary>
        public Func<ISoapClient, OnSoapEnvelopeResponseArguments, CancellationToken, Task> OnSoapEnvelopeResponseAsyncAction { get; set; }

        #region Implementation of ISoapHandler

        /// <summary>
        /// The order for which the handler will be executed
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void OnSoapEnvelopeRequest(ISoapClient client, OnSoapEnvelopeRequestArguments arguments)
        {
            OnSoapEnvelopeRequestAction?.Invoke(client, arguments);
        }

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public async Task OnSoapEnvelopeRequestAsync(ISoapClient client, OnSoapEnvelopeRequestArguments arguments, CancellationToken ct)
        {
            if (OnSoapEnvelopeRequestAsyncAction != null)
                await OnSoapEnvelopeRequestAsyncAction(client, arguments, ct);
        }

        /// <summary>
        /// Method invoked before sending the <see cref="HttpRequestMessage"/> to the server.
        /// Useful to log the request or change properties like HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void OnHttpRequest(ISoapClient client, OnHttpRequestArguments arguments)
        {
            OnHttpRequestAction?.Invoke(client, arguments);
        }

        /// <summary>
        /// Method invoked before sending the <see cref="HttpRequestMessage"/> to the server.
        /// Useful to log the request or change properties like HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public async Task OnHttpRequestAsync(ISoapClient client, OnHttpRequestArguments arguments, CancellationToken ct)
        {
            if (OnHttpRequestAsyncAction != null)
                await OnHttpRequestAsyncAction(client, arguments, ct);
        }

        /// <summary>
        /// Method invoked after receiving a <see cref="HttpResponseMessage"/> from the server.
        /// Useful to log the response or validate HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void OnHttpResponse(ISoapClient client, OnHttpResponseArguments arguments)
        {
            OnHttpResponseAction?.Invoke(client, arguments);
        }

        /// <summary>
        /// Method invoked after receiving a <see cref="HttpResponseMessage"/> from the server.
        /// Useful to log the response or validate HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public async Task OnHttpResponseAsync(ISoapClient client, OnHttpResponseArguments arguments, CancellationToken ct)
        {
            if (OnHttpResponseAsyncAction != null)
                await OnHttpResponseAsyncAction(client, arguments, ct);
        }

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void OnSoapEnvelopeResponse(ISoapClient client, OnSoapEnvelopeResponseArguments arguments)
        {
            OnSoapEnvelopeResponseAction?.Invoke(client, arguments);
        }

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Task to be awaited</returns>
        public async Task OnSoapEnvelopeResponseAsync(ISoapClient client, OnSoapEnvelopeResponseArguments arguments, CancellationToken ct)
        {
            if (OnSoapEnvelopeResponseAsyncAction != null)
                await OnSoapEnvelopeResponseAsyncAction(client, arguments, ct);
        }

        #endregion
    }
}
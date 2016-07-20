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
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using Handlers;
    using Helpers;
    using Models;

    /// <summary>
    /// The SOAP client that can be used to invoke SOAP Endpoints
    /// </summary>
    public class SoapClient : ISoapClient, IDisposable
    {
        private readonly bool _disposeHttpClient = true;
        private readonly List<ISoapHandler> _handlers = new List<ISoapHandler>();

        /// <summary>
        /// The used HTTP client
        /// </summary>
        public HttpClient HttpClient { get; }

        #region Constructors

        /// <summary>
        /// Creates a new SOAP Client
        /// </summary>
        public SoapClient()
            : this(new HttpClient())
        {

        }

        /// <summary>
        /// Creates a new SOAP Client
        /// </summary>
        /// <param name="handler">The handler to be used by the <see cref="HttpClient"/></param>
        public SoapClient(HttpMessageHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            HttpClient = new HttpClient(handler);
        }

        /// <summary>
        /// Creates a new SOAP Client
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> to be used</param>
        /// <param name="disposeHttpClient">Should the client also be disposed</param>
        public SoapClient(HttpClient httpClient, bool disposeHttpClient = true)
        {
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));

            HttpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
        }

        /// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
        ~SoapClient()
        {
            Dispose(false);
        }

        #endregion

        #region Implementation of ISoapClient

        /// <summary>
        /// The handler
        /// </summary>
        public IReadOnlyCollection<ISoapHandler> Handlers => _handlers;

        /// <summary>
        /// Indicates if the XML declaration should be removed from the
        /// serialized SOAP Envelopes
        /// </summary>
        public bool RemoveXmlDeclaration { get; set; } = true;

        #region Send

        /// <summary>
        /// Sends the given <see cref="SoapEnvelope"/> into the specified url.
        /// </summary>
        /// <param name="url">The url that will receive the request</param>
        /// <param name="action">The SOAP action beeing performed</param>
        /// <param name="requestEnvelope">The <see cref="SoapEnvelope"/> to be sent</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited for the result</returns>
        /// <exception cref="SoapEnvelopeSerializationException"></exception>
        /// <exception cref="SoapEnvelopeDeserializationException"></exception>
        public virtual async Task<SoapEnvelope> SendAsync(
            string url, string action, SoapEnvelope requestEnvelope, CancellationToken ct = default(CancellationToken))
        {
            var trackingId = Guid.NewGuid();
            var handlersOrderedAsc = _handlers.OrderBy(e => e.Order).ToList();

            var beforeSoapEnvelopeSerializationHandlersResult =
                RunBeforeSoapEnvelopeSerializationHandlers(
                    requestEnvelope, url, action, trackingId, handlersOrderedAsc);

            string requestXml;
            try
            {
                requestXml = beforeSoapEnvelopeSerializationHandlersResult.Envelope.ToXmlString(RemoveXmlDeclaration);
            }
            catch (Exception e)
            {
                throw new SoapEnvelopeSerializationException(requestEnvelope, e);
            }

            var beforeHttpRequestHandlersResult =
                RunBeforeHttpRequestHandlers(
                    requestXml, url, action, trackingId,
                    beforeSoapEnvelopeSerializationHandlersResult.State, handlersOrderedAsc);

            var response =
                await HttpClient.SendAsync(beforeHttpRequestHandlersResult.Request, ct).ConfigureAwait(false);

            var handlersOrderedDesc = _handlers.OrderByDescending(e => e.Order).ToList();

            var afterHttpResponseHandlersResult =
                RunAfterHttpResponseHandlers(
                    response, url, action, trackingId, beforeHttpRequestHandlersResult.State, handlersOrderedDesc);

            var responseXml =
                await afterHttpResponseHandlersResult.Response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(responseXml))
                throw new SoapEnvelopeDeserializationException(responseXml, "The response content is empty.");
            SoapEnvelope responseEnvelope;
            try
            {
                responseEnvelope = responseXml.ToObject<SoapEnvelope>();
            }
            catch (Exception e)
            {
                throw new SoapEnvelopeDeserializationException(responseXml, e);
            }

            var afterSoapEnvelopeDeserializationHandlerResult =
                RunAfterSoapEnvelopeDeserializationHandler(
                    responseEnvelope, url, action, trackingId,
                    afterHttpResponseHandlersResult.State, handlersOrderedDesc);

            return afterSoapEnvelopeDeserializationHandlerResult.Envelope;
        }

        /// <summary>
        /// Sends the given <see cref="SoapEnvelope"/> into the specified url.
        /// </summary>
        /// <param name="url">The url that will receive the request</param>
        /// <param name="action">The SOAP action beeing performed</param>
        /// <param name="requestEnvelope">The <see cref="SoapEnvelope"/> to be sent</param>
        /// <returns>The resulting <see cref="SoapEnvelope"/></returns>
        /// <exception cref="SoapEnvelopeSerializationException"></exception>
        /// <exception cref="SoapEnvelopeDeserializationException"></exception>
        public virtual SoapEnvelope Send(string url, string action, SoapEnvelope requestEnvelope)
        {
            return SendAsync(url, action, requestEnvelope).Result;
        }

        #endregion

        /// <summary>
        /// Adds the <see cref="ISoapHandler"/> to the <see cref="Handlers"/> collection.
        /// </summary>
        /// <param name="handler">The handler to add</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddHandler(ISoapHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            _handlers.Add(handler);
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the underline <see cref="HttpClient"/>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _disposeHttpClient)
                HttpClient.Dispose();
        }

        #endregion

        #region Prepare

        /// <summary>
        /// Prepares a new <see cref="SoapClient"/> instance to be configured.
        /// </summary>
        /// <returns>The SOAP client to be configured</returns>
        public static SoapClient Prepare()
        {
            return new SoapClient();
        }

        /// <summary>
        /// Prepares a new <see cref="SoapClient"/> instance to be configured.
        /// </summary>
        /// <param name="handler">The handler to be used by the <see cref="HttpClient"/></param>
        /// <returns>The SOAP client to be configured</returns>
        public static SoapClient Prepare(HttpMessageHandler handler)
        {
            return new SoapClient(handler);
        }

        /// <summary>
        /// Prepares a new <see cref="SoapClient"/> instance to be configured.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> to be used</param>
        /// <param name="disposeHttpClient">Should the client also be disposed</param>
        /// <returns>The SOAP client to be configured</returns>
        public static SoapClient Prepare(HttpClient httpClient, bool disposeHttpClient = true)
        {
            return new SoapClient(httpClient, disposeHttpClient);
        }

        #endregion

        #region Private

        private OnSoapEnvelopeRequestArguments RunBeforeSoapEnvelopeSerializationHandlers(
            SoapEnvelope envelope, string url, string action, Guid trackingId, IEnumerable<ISoapHandler> handlers)
        {
            var beforeSoapEnvelopeSerializationArg =
                new OnSoapEnvelopeRequestArguments(envelope, url, action, trackingId);
            foreach (var handler in handlers)
                handler.OnSoapEnvelopeRequest(this, beforeSoapEnvelopeSerializationArg);

            return beforeSoapEnvelopeSerializationArg;
        }

        private OnHttpRequestArguments RunBeforeHttpRequestHandlers(
            string xml, string url, string action, Guid trackingId, object state, IEnumerable<ISoapHandler> handlers)
        {
            var beforeHttpRequestArguments =
                new OnHttpRequestArguments(new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(xml, Encoding.UTF8, "text/xml")
                }, url, action, trackingId)
                {
                    State = state
                };
            foreach (var handler in handlers)
                handler.OnHttpRequest(this, beforeHttpRequestArguments);

            return beforeHttpRequestArguments;
        }

        private OnHttpResponseArguments RunAfterHttpResponseHandlers(
            HttpResponseMessage response, string url, string action, Guid trackingId, object state, IEnumerable<ISoapHandler> handlers)
        {
            var afterHttpResponseArguments =
                new OnHttpResponseArguments(response, url, action, trackingId)
                {
                    State = state
                };
            foreach (var handler in handlers)
                handler.OnHttpResponse(this, afterHttpResponseArguments);

            return afterHttpResponseArguments;
        }

        private OnSoapEnvelopeResponseArguments RunAfterSoapEnvelopeDeserializationHandler(
            SoapEnvelope envelope, string url, string action, Guid trackingId, object state, IEnumerable<ISoapHandler> handlers)
        {
            var afterSoapEnvelopeDeserializationArguments =
                new OnSoapEnvelopeResponseArguments(envelope, url, action, trackingId)
                {
                    State = state
                };
            foreach (var handler in handlers)
                handler.OnSoapEnvelopeResponse(this, afterSoapEnvelopeDeserializationArguments);

            return afterSoapEnvelopeDeserializationArguments;
        }

        #endregion
    }
}

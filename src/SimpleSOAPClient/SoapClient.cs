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
        private readonly List<Func<ISoapClient, IRequestEnvelopeHandlerData, IRequestEnvelopeHandlerResult>> _requestEnvelopeHandlers = new List<Func<ISoapClient, IRequestEnvelopeHandlerData, IRequestEnvelopeHandlerResult>>();
        private readonly List<Func<ISoapClient, IRequestRawHandlerData, IRequestRawHandlerResult>> _requestRawHandlers = new List<Func<ISoapClient, IRequestRawHandlerData, IRequestRawHandlerResult>>();
        private readonly List<Func<ISoapClient, IResponseRawHandlerData, IResponseRawHandlerResult>> _responseRawHandlers = new List<Func<ISoapClient, IResponseRawHandlerData, IResponseRawHandlerResult>>();
        private readonly List<Func<ISoapClient, IResponseEnvelopeHandlerData, IResponseEnvelopeHandlerResult>> _responseEnvelopeHandlers = new List<Func<ISoapClient, IResponseEnvelopeHandlerData, IResponseEnvelopeHandlerResult>>();
        private readonly bool _disposeHttpClient = true;

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

        ~SoapClient()
        {
            Dispose(false);
        }

        #endregion

        #region Implementation of ISoapClient

        #region Handlers

        /// <summary>
        /// Handler collection that can manipulate the <see cref="SoapEnvelope"/>
        /// before serialization.
        /// </summary>
        public IEnumerable<Func<ISoapClient, IRequestEnvelopeHandlerData, IRequestEnvelopeHandlerResult>> RequestEnvelopeHandlers => _requestEnvelopeHandlers;

        /// <summary>
        /// Handler collection that can manipulate the generated XML string.
        /// </summary>
        public IEnumerable<Func<ISoapClient, IRequestRawHandlerData, IRequestRawHandlerResult>> RequestRawHandlers => _requestRawHandlers;

        /// <summary>
        /// Handler collection that can manipulate the returned string before deserialization.
        /// </summary>
        public IEnumerable<Func<ISoapClient, IResponseRawHandlerData, IResponseRawHandlerResult>> ResponseRawHandlers => _responseRawHandlers;

        /// <summary>
        /// Handler collection that can manipulate the <see cref="SoapEnvelope"/> returned
        /// by the SOAP Endpoint.
        /// </summary>
        public IEnumerable<Func<ISoapClient, IResponseEnvelopeHandlerData, IResponseEnvelopeHandlerResult>> ResponseEnvelopeHandlers => _responseEnvelopeHandlers;

        #endregion

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
            var request = CreateHttpRequestMessage(url, action, requestEnvelope);

            var result = await HttpClient.SendAsync(request, ct);
            
            var responseXml = await result.Content.ReadAsStringAsync();

            return CreateSoapEnvelope(url, action, result, responseXml);
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

        #region AddHandler

        /// <summary>
        /// Appends the given handler to the <see cref="RequestEnvelopeHandlers"/> collection.
        /// </summary>
        /// <param name="handler">The handler to append</param>
        public void AddRequestEnvelopeHandler(
            Func<ISoapClient, IRequestEnvelopeHandlerData, IRequestEnvelopeHandlerResult> handler)
        {
            _requestEnvelopeHandlers.Add(handler);
        }

        /// <summary>
        /// Appends the given handler to the <see cref="RequestRawHandlers"/> collection.
        /// </summary>
        /// <param name="handler">The handler to append</param>
        public void AddRequestRawHandler(
            Func<ISoapClient, IRequestRawHandlerData, IRequestRawHandlerResult> handler)
        {
            _requestRawHandlers.Add(handler);
        }

        /// <summary>
        /// Appends the given handler to the <see cref="ResponseRawHandlers"/> collection.
        /// </summary>
        /// <param name="handler">The handler to append</param>
        public void AddResponseRawHandler(
            Func<ISoapClient, IResponseRawHandlerData, IResponseRawHandlerResult> handler)
        {
            _responseRawHandlers.Add(handler);
        }

        /// <summary>
        /// Appends the given handler to the <see cref="ResponseEnvelopeHandlers"/> collection.
        /// </summary>
        /// <param name="handler">The handler to append</param>
        public void AddResponseEnvelopeHandler(
            Func<ISoapClient, IResponseEnvelopeHandlerData, IResponseEnvelopeHandlerResult> handler)
        {
            _responseEnvelopeHandlers.Add(handler);
        }

        #endregion

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        private HttpRequestMessage CreateHttpRequestMessage(
            string url, string action, SoapEnvelope requestEnvelope)
        {
            var requestEnvelopeHandlerData = new RequestEnvelopeHandlerData(url, action, requestEnvelope);
            foreach (var handler in RequestEnvelopeHandlers)
            {
                var result = handler(this, requestEnvelopeHandlerData);
                requestEnvelopeHandlerData = new RequestEnvelopeHandlerData(url, action, result.Envelope);
                if (result.CancelHandlerFlow)
                    break;
            }

            string requestXml;
            try
            {
                requestXml = requestEnvelopeHandlerData.Envelope.ToXmlString(RemoveXmlDeclaration);
            }
            catch (Exception e)
            {
                throw new SoapEnvelopeSerializationException(requestEnvelope, e);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(requestXml, Encoding.UTF8, "text/xml")
            };
            request.Headers.Add("SOAPAction", action);

            var requestRawHandlerData = new RequestRawHandlerData(url, action, request, requestXml);
            foreach (var handler in RequestRawHandlers)
            {
                var result = handler(this, requestRawHandlerData);
                requestRawHandlerData = new RequestRawHandlerData(url, action, result.Request, result.Content);
                if (result.CancelHandlerFlow)
                    break;
            }
            requestRawHandlerData.Request.Content =
                new StringContent(requestRawHandlerData.Content, Encoding.UTF8, "text/xml");

            return requestRawHandlerData.Request;
        }

        private SoapEnvelope CreateSoapEnvelope(string url, string action, HttpResponseMessage response, string responseXml)
        {
            var responseRawHandlerData = new ResponseRawHandlerData(url, action, response, responseXml);
            foreach (var handler in ResponseRawHandlers)
            {
                var result = handler(this, responseRawHandlerData);
                responseRawHandlerData = new ResponseRawHandlerData(url, action, result.Response, result.Content);
                if (result.CancelHandlerFlow)
                    break;
            }

            if (string.IsNullOrWhiteSpace(responseRawHandlerData.Content))
                throw new SoapEnvelopeDeserializationException(
                    responseRawHandlerData.Content, "The response content is empty.");

            SoapEnvelope responseEnvelope;
            try
            {
                responseEnvelope = responseRawHandlerData.Content.ToObject<SoapEnvelope>();
            }
            catch (Exception e)
            {
                throw new SoapEnvelopeDeserializationException(responseRawHandlerData.Content, e);
            }

            var responseEnvelopeHandlerData = new ResponseEnvelopeHandlerData(url, action, responseEnvelope);
            foreach (var handler in ResponseEnvelopeHandlers)
            {
                var result = handler(this, responseEnvelopeHandlerData);
                responseEnvelopeHandlerData = new ResponseEnvelopeHandlerData(url, action, result.Envelope);
                if (result.CancelHandlerFlow)
                    break;
            }

            return responseEnvelopeHandlerData.Envelope;
        }

        #endregion
    }
}

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
        public SoapClient(HttpClient httpClient)
        {
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));

            HttpClient = httpClient;
        }

        ~SoapClient()
        {
            Dispose(false);
        }

        #endregion
        
        #region Implementation of ISoapClient

        /// <summary>
        /// Handler collection that can manipulate the <see cref="SoapEnvelope"/>
        /// before serialization.
        /// </summary>
        public ICollection<Action<ISoapClient, IRequestEnvelopeHandlerData>> RequestEnvelopeHandlers { get; } = 
            new List<Action<ISoapClient, IRequestEnvelopeHandlerData>>();

        /// <summary>
        /// Handler collection that can manipulate the generated XML string.
        /// </summary>
        public ICollection<Action<ISoapClient, IRequestRawHandlerData>> RequestRawHandlers { get; } =
            new List<Action<ISoapClient, IRequestRawHandlerData>>();

        /// <summary>
        /// Handler collection that can manipulate the <see cref="SoapEnvelope"/> returned
        /// by the SOAP Endpoint.
        /// </summary>
        public ICollection<Action<ISoapClient, IResponseEnvelopeHandlerData>> ResponseEnvelopeHandlers { get; } =
            new List<Action<ISoapClient, IResponseEnvelopeHandlerData>>();

        /// <summary>
        /// Handler collection that can manipulate the returned string before deserialization.
        /// </summary>
        public ICollection<Action<ISoapClient, IResponseRawHandlerData>> ResponseRawHandlers { get; } =
            new List<Action<ISoapClient, IResponseRawHandlerData>>();

        /// <summary>
        /// Indicates if the XML declaration should be removed from the
        /// serialized SOAP Envelopes
        /// </summary>
        public bool RemoveXmlDeclaration { get; set; } = true;

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
#if NET40
        public virtual Task<SoapEnvelope> SendAsync(
            string url, string action, SoapEnvelope requestEnvelope, CancellationToken ct = default(CancellationToken))
        {
            var request = CreateHttpRequestMessage(url, action, requestEnvelope);

            var cts = new TaskCompletionSource<SoapEnvelope>();

            HttpClient
                .SendAsync(request, ct)
                .ContinueWith(t01 =>
                {
                    if (t01.IsFaulted)
                    {
                        cts.SetExceptionFromTask(t01);
                    }
                    else if (t01.IsCompleted)
                    {
                        var result = t01.Result;
                        result.Content.ReadAsStringAsync().ContinueWith(t02 =>
                        {
                            if (t02.IsFaulted)
                            {
                                cts.SetExceptionFromTask(t02);
                            }
                            else if (t02.IsCompleted)
                            {
                                try
                                {
                                    cts.SetResult(CreateSoapEnvelope(url, action, result, t02.Result));
                                }
                                catch (Exception e)
                                {
                                    cts.SetException(e);
                                }
                            }
                            else
                            {
                                cts.SetCanceled();
                            }
                        }, ct);
                    }
                    else
                    {
                        cts.SetCanceled();
                    }
                }, ct);

            return cts.Task;
        }
#else
        public virtual async Task<SoapEnvelope> SendAsync(
            string url, string action, SoapEnvelope requestEnvelope, CancellationToken ct = default(CancellationToken))
        {
            var request = CreateHttpRequestMessage(url, action, requestEnvelope);

            var result = await HttpClient.SendAsync(request, ct);
            
            var responseXml = await result.Content.ReadAsStringAsync();

            return CreateSoapEnvelope(url, action, result, responseXml);
        }
#endif

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

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                HttpClient.Dispose();
        }

        #endregion

        #region Prepare

        public static SoapClient Prepare()
        {
            return new SoapClient();
        }

        public static SoapClient Prepare(HttpMessageHandler handler)
        {
            return new SoapClient(handler);
        }

        public static SoapClient Prepare(HttpClient httpClient)
        {
            return new SoapClient(httpClient);
        }

        #endregion

        #region Private

        private HttpRequestMessage CreateHttpRequestMessage(
            string url, string action, SoapEnvelope requestEnvelope)
        {
            var requestEnvelopeHandlerData = new RequestEnvelopeHandlerData(url, action, requestEnvelope);
            foreach (var handler in RequestEnvelopeHandlers)
            {
                if (requestEnvelopeHandlerData.CancelHandlerFlow)
                    break;
                handler(this, requestEnvelopeHandlerData);
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
                if(requestRawHandlerData.CancelHandlerFlow)
                    break;
                handler(this, requestRawHandlerData);
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
                if(responseRawHandlerData.CancelHandlerFlow)
                    break;
                handler(this, responseRawHandlerData);
            }

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
                if(responseEnvelopeHandlerData.CancelHandlerFlow)
                    break;
                handler(this, responseEnvelopeHandlerData);
            }

            return responseEnvelopeHandlerData.Envelope;
        }

        #endregion
    }
}

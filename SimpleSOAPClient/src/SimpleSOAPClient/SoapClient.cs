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
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Helpers;
    using Models;

    /// <summary>
    /// The SOAP client that can be used to invoke SOAP Endpoints
    /// </summary>
    public class SoapClient : IDisposable
    {
        #region Constructors

        public SoapClient()
            : this(new HttpClient())
        {

        }

        public SoapClient(HttpMessageHandler handler)
            : this(new HttpClient(handler))
        {

        }

        public SoapClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        ~SoapClient()
        {
            Dispose(false);
        }

        #endregion

        public HttpClient HttpClient { get; }

        public Func<string, SoapEnvelope, SoapEnvelope> RequestEnvelopeHandler { get; set; }

        public Func<string, string> RequestRawHandler { get; set; }

        public Func<string, SoapEnvelope, SoapEnvelope> ResponseEnvelopeHandler { get; set; }

        public Func<string, string> ResponseRawHandler { get; set; }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
                HttpClient.Dispose();
        }

        #endregion

        public async Task<SoapEnvelope> SendAsync(
            string url, SoapEnvelope requestEnvelope, CancellationToken ct = default(CancellationToken))
        {
            if (RequestEnvelopeHandler != null)
                requestEnvelope = RequestEnvelopeHandler(url, requestEnvelope);

            var requestXml = requestEnvelope.ToXmlString();
            if (RequestRawHandler != null)
                requestXml = RequestRawHandler(requestXml);

            var result =
                await HttpClient.PostAsync(
                    url, new StringContent(requestXml, Encoding.UTF8, "text/xml"), ct);

            var responseXml = await result.Content.ReadAsStringAsync();
            if (ResponseRawHandler != null)
                responseXml = ResponseRawHandler(responseXml);

            var responseEnvelope = responseXml.ToObject<SoapEnvelope>();

            if (ResponseEnvelopeHandler != null)
                responseEnvelope = ResponseEnvelopeHandler(url, responseEnvelope);

            return responseEnvelope;
        }

        public static SoapClient Prepare()
        {
            return new SoapClient();
        }
    }
}

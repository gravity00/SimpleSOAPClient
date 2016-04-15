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
namespace SimpleSOAPClient.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Models;

    /// <summary>
    /// Helper methods for working with <see cref="ISoapClient"/> instances.
    /// </summary>
    public static class ClientHelpers
    {
        #region UsingRequestEnvelopeHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestEnvelopeHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestEnvelopeHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, SoapEnvelope, SoapEnvelope>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) return client;
            
            var currentHandler = client.RequestEnvelopeHandler;
            client.RequestEnvelopeHandler = (url, action, envelope) =>
            {
                foreach (var handler in handlers)
                {
                    envelope = handler(url, action, envelope);
                }
                return currentHandler == null ? envelope : currentHandler(url, action, envelope);
            };

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestEnvelopeHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestEnvelopeHandler<TSoapClient>(
            this TSoapClient client, params Func<string, string, SoapEnvelope, SoapEnvelope>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingRequestEnvelopeHandler(
                (IEnumerable<Func<string, string, SoapEnvelope, SoapEnvelope>>) handlers);
        }

        #endregion

        #region UsingRequestRawHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestRawHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestRawHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, HttpRequestMessage, string, string>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));

            var currentHandler = client.RequestRawHandler;
            client.RequestRawHandler = (url, action, request, xml) =>
            {
                foreach (var handler in handlers)
                {
                    xml = handler(url, action, request, xml);
                }
                return currentHandler == null ? xml : currentHandler(url, action, request, xml);
            };

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestRawHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestRawHandler<TSoapClient>(
            this TSoapClient client, params Func<string, string, HttpRequestMessage, string, string>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingRequestRawHandler(
                (IEnumerable<Func<string, string, HttpRequestMessage, string, string>>) handlers);
        }

        #endregion

        #region UsingResponseRawHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseRawHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseRawHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, HttpResponseMessage, string, string>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));

            var currentHandler = client.ResponseRawHandler;
            client.ResponseRawHandler = (url, action, response, xml) =>
            {
                foreach (var handler in handlers)
                {
                    xml = handler(url, action, response, xml);
                }
                return currentHandler == null ? xml : currentHandler(url, action, response, xml);
            };

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseRawHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseRawHandler<TSoapClient>(
            this TSoapClient client, params Func<string, string, HttpResponseMessage, string, string>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingResponseRawHandler(
                (IEnumerable<Func<string, string, HttpResponseMessage, string, string>>) handlers);
        }

        #endregion

        #region UsingResponseEnvelopeHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseEnvelopeHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseEnvelopeHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, SoapEnvelope, SoapEnvelope>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) return client;

            var currentHandler = client.ResponseEnvelopeHandler;
            client.ResponseEnvelopeHandler = (url, action, envelope) =>
            {
                foreach (var handler in handlers)
                {
                    envelope = handler(url, action, envelope);
                }
                return currentHandler == null ? envelope : currentHandler(url, action, envelope);
            };

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseEnvelopeHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseEnvelopeHandler<TSoapClient>(
            this TSoapClient client, params Func<string, string, SoapEnvelope, SoapEnvelope>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingResponseEnvelopeHandler(
                (IEnumerable<Func<string, string, SoapEnvelope, SoapEnvelope>>)handlers);
        }

        #endregion
    }
}

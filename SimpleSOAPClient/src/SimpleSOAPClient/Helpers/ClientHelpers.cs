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
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestEnvelopeHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, SoapEnvelope, SoapEnvelope>> handlers, bool append = false)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) return client;
            
            var currentHandler = client.RequestEnvelopeHandler;
            if (append)
            {
                client.RequestEnvelopeHandler = (url, action, envelope) =>
                {
                    envelope = currentHandler == null ? envelope : currentHandler(url, action, envelope);
                    foreach (var handler in handlers)
                    {
                        envelope = handler(url, action, envelope);
                    }
                    return envelope;
                };
            }
            else
            {
                client.RequestEnvelopeHandler = (url, action, envelope) =>
                {
                    foreach (var handler in handlers)
                    {
                        envelope = handler(url, action, envelope);
                    }
                    return currentHandler == null ? envelope : currentHandler(url, action, envelope);
                };
            }

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

            return client.UsingRequestEnvelopeHandler(handlers, false);
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestEnvelopeHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestEnvelopeHandler<TSoapClient>(
            this TSoapClient client, bool append, params Func<string, string, SoapEnvelope, SoapEnvelope>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingRequestEnvelopeHandler(handlers, append);
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
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestRawHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, HttpRequestMessage, string, string>> handlers, bool append = false)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));

            var currentHandler = client.RequestRawHandler;
            if (append)
            {
                client.RequestRawHandler = (url, action, request, xml) =>
                {
                    xml = currentHandler == null ? xml : currentHandler(url, action, request, xml);
                    foreach (var handler in handlers)
                    {
                        xml = handler(url, action, request, xml);
                    }
                    return xml;
                };
            }
            else
            {
                client.RequestRawHandler = (url, action, request, xml) =>
                {
                    foreach (var handler in handlers)
                    {
                        xml = handler(url, action, request, xml);
                    }
                    return currentHandler == null ? xml : currentHandler(url, action, request, xml);
                };
            }

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

            return client.UsingRequestRawHandler(handlers, false);
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestRawHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestRawHandler<TSoapClient>(
            this TSoapClient client, bool append, params Func<string, string, HttpRequestMessage, string, string>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingRequestRawHandler(handlers, append);
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
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseRawHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, HttpResponseMessage, string, string>> handlers, bool append = false)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));

            var currentHandler = client.ResponseRawHandler;
            if (append)
            {
                client.ResponseRawHandler = (url, action, response, xml) =>
                {
                    xml = currentHandler == null ? xml : currentHandler(url, action, response, xml);
                    foreach (var handler in handlers)
                    {
                        xml = handler(url, action, response, xml);
                    }
                    return xml;
                };
            }
            else
            {
                client.ResponseRawHandler = (url, action, response, xml) =>
                {
                    foreach (var handler in handlers)
                    {
                        xml = handler(url, action, response, xml);
                    }
                    return currentHandler == null ? xml : currentHandler(url, action, response, xml);
                };
            }

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

            return client.UsingResponseRawHandler(handlers, false);
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseRawHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseRawHandler<TSoapClient>(
            this TSoapClient client, bool append, params Func<string, string, HttpResponseMessage, string, string>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingResponseRawHandler(handlers, append);
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
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseEnvelopeHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<string, string, SoapEnvelope, SoapEnvelope>> handlers, bool append = false)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null) return client;

            var currentHandler = client.ResponseEnvelopeHandler;
            if (append)
            {
                client.ResponseEnvelopeHandler = (url, action, envelope) =>
                {
                    envelope = currentHandler == null ? envelope : currentHandler(url, action, envelope);
                    foreach (var handler in handlers)
                    {
                        envelope = handler(url, action, envelope);
                    }
                    return envelope;
                };
            }
            else
            {
                client.ResponseEnvelopeHandler = (url, action, envelope) =>
                {
                    foreach (var handler in handlers)
                    {
                        envelope = handler(url, action, envelope);
                    }
                    return currentHandler == null ? envelope : currentHandler(url, action, envelope);
                };
            }

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

            return client.UsingResponseEnvelopeHandler(handlers, false);
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseEnvelopeHandler"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="append">Indicates if the handlers must be appended to existing one. By default they will be prepended.</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseEnvelopeHandler<TSoapClient>(
            this TSoapClient client, bool append, params Func<string, string, SoapEnvelope, SoapEnvelope>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0) return client;

            return client.UsingResponseEnvelopeHandler(handlers, append);
        }

        #endregion

        /// <summary>
        /// Should the XML declaration be removed from the resulting deserialization?
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="remove">Should the XML declaration be removed? Defaults to true</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSerializeRemoveXmlDeclaration<TSoapClient>(this TSoapClient client, bool remove = true)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            client.RemoveXmlDeclaration = remove;
            return client;
        }
    }
}

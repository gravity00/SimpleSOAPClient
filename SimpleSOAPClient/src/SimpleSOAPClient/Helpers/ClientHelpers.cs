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
    using Handlers;

    /// <summary>
    /// Helper methods for working with <see cref="ISoapClient"/> instances.
    /// </summary>
    public static class ClientHelpers
    {
        #region UsingRequestEnvelopeHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestEnvelopeHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestEnvelopeHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<ISoapClient, IRequestEnvelopeHandlerData, IRequestEnvelopeHandlerResult>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null)
                return client;

            foreach (var handler in handlers)
                client.AddRequestEnvelopeHandler(handler);

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestEnvelopeHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestEnvelopeHandler<TSoapClient>(
            this TSoapClient client, params Func<ISoapClient, IRequestEnvelopeHandlerData, IRequestEnvelopeHandlerResult>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0)
                return client;

            foreach (var handler in handlers)
                client.AddRequestEnvelopeHandler(handler);

            return client;
        }

        #endregion

        #region UsingRequestRawHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestRawHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestRawHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<ISoapClient, IRequestRawHandlerData, IRequestRawHandlerResult>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null)
                return client;

            foreach (var handler in handlers)
                client.AddRequestRawHandler(handler);

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.RequestRawHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingRequestRawHandler<TSoapClient>(
            this TSoapClient client, params Func<ISoapClient, IRequestRawHandlerData, IRequestRawHandlerResult>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0)
                return client;

            foreach (var handler in handlers)
                client.AddRequestRawHandler(handler);

            return client;
        }

        #endregion

        #region UsingResponseRawHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseRawHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseRawHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<ISoapClient, IResponseRawHandlerData, IResponseRawHandlerResult>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null)
                return client;

            foreach (var handler in handlers)
                client.AddResponseRawHandler(handler);

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseRawHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseRawHandler<TSoapClient>(
            this TSoapClient client, params Func<ISoapClient, IResponseRawHandlerData, IResponseRawHandlerResult>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0)
                return client;

            foreach (var handler in handlers)
                client.AddResponseRawHandler(handler);

            return client;
        }

        #endregion

        #region UsingResponseEnvelopeHandler

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseEnvelopeHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseEnvelopeHandler<TSoapClient>(
            this TSoapClient client, IEnumerable<Func<ISoapClient, IResponseEnvelopeHandlerData, IResponseEnvelopeHandlerResult>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null)
                return client;

            foreach (var handler in handlers)
                client.AddResponseEnvelopeHandler(handler);

            return client;
        }

        /// <summary>
        /// Attaches the provided collection handler collection to the 
        /// <see cref="ISoapClient.ResponseEnvelopeHandlers"/> creating a pipeline.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handlers">The handler collection to attach as a pipeline</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingResponseEnvelopeHandler<TSoapClient>(
            this TSoapClient client, params Func<ISoapClient, IResponseEnvelopeHandlerData, IResponseEnvelopeHandlerResult>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0)
                return client;

            foreach (var handler in handlers)
                client.AddResponseEnvelopeHandler(handler);

            return client;
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

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
    
    public static partial class ClientHelpers
    {
        #region Result

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

        #region Void

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
            this TSoapClient client, IEnumerable<Action<ISoapClient, IRequestEnvelopeHandlerData>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null)
                return client;

            foreach (var handler in handlers)
                client.AddRequestEnvelopeHandler((c, data) =>
                {
                    handler(c, data);
                    return Handling.ProceedRequestEnvelopeHandlerFlowWith(data);
                });

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
            this TSoapClient client, params Action<ISoapClient, IRequestEnvelopeHandlerData>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0)
                return client;

            foreach (var handler in handlers)
                client.AddRequestEnvelopeHandler((c, data) =>
                {
                    handler(c, data);
                    return Handling.ProceedRequestEnvelopeHandlerFlowWith(data);
                });

            return client;
        }

        #endregion

        #region Boolean

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
            this TSoapClient client, IEnumerable<Func<ISoapClient, IRequestEnvelopeHandlerData, bool>> handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null)
                return client;

            foreach (var handler in handlers)
                client.AddRequestEnvelopeHandler(
                    (c, data) => new RequestEnvelopeHandlerResult(handler(c, data), data.Envelope));

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
            this TSoapClient client, params Func<ISoapClient, IRequestEnvelopeHandlerData, bool>[] handlers)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handlers == null || handlers.Length == 0)
                return client;

            foreach (var handler in handlers)
                client.AddRequestEnvelopeHandler(
                    (c, data) => new RequestEnvelopeHandlerResult(handler(c, data), data.Envelope));

            return client;
        }

        #endregion
    }
}

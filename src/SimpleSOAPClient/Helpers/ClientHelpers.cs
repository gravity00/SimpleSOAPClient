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
    using Handlers;

    /// <summary>
    /// Helper methods for working with <see cref="ISoapClient"/> instances.
    /// </summary>
    public static class ClientHelpers
    {
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

        #region Handlers

        /// <summary>
        /// Adds the given handler to the SOAP client
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="handler">The handler to add</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient WithHandler<TSoapClient>(this TSoapClient client, ISoapHandler handler)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            client.AddHandler(handler);
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeRequest"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeRequest<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnSoapEnvelopeRequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeRequestDelegate = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnHttpRequest"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnHttpRequest<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnHttpRequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpRequestDelegate = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnHttpResponse"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnHttpResponse<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnHttpResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpResponseDelegate = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeResponse"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeResponse<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnSoapEnvelopeResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeResponseDelegate = action
            });
            return client;
        }

        #endregion
    }
}

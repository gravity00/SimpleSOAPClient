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
    using System.Threading;
    using System.Threading.Tasks;
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

        #region OnSoapEnvelopeRequest

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
                OnSoapEnvelopeRequestAction = action
            });
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
            this TSoapClient client, Action<OnSoapEnvelopeRequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeRequestAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeRequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeRequest<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnSoapEnvelopeRequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeRequestAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeRequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeRequest<TSoapClient>(
            this TSoapClient client, Func<OnSoapEnvelopeRequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeRequestAsyncAction = async (c, args, ct) => await action(args, ct)
            });
            return client;
        }

        #endregion

        #region OnHttpRequest

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
                OnHttpRequestAction = action
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
            this TSoapClient client, Action<OnHttpRequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpRequestAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnHttpRequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnHttpRequest<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnHttpRequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpRequestAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnHttpRequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnHttpRequest<TSoapClient>(
            this TSoapClient client, Func<OnHttpRequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpRequestAsyncAction = async (c, args, ct) => await action(args, ct)
            });
            return client;
        }

        #endregion

        #region OnHttpResponse

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
                OnHttpResponseAction = action
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
            this TSoapClient client, Action<OnHttpResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpResponseAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnHttpResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnHttpResponse<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnHttpResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpResponseAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnHttpResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnHttpResponse<TSoapClient>(
            this TSoapClient client, Func<OnHttpResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnHttpResponseAsyncAction = async (c, args, ct) => await action(args, ct)
            });
            return client;
        }

        #endregion

        #region OnSoapEnvelopeResponse

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
                OnSoapEnvelopeResponseAction = action
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
            this TSoapClient client, Action<OnSoapEnvelopeResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeResponseAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeResponse<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnSoapEnvelopeResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeResponseAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeResponse<TSoapClient>(
            this TSoapClient client, Func<OnSoapEnvelopeResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeResponseAsyncAction = async (c, args, ct) => await action(args, ct)
            });
            return client;
        }

        #endregion

        #endregion
    }
}

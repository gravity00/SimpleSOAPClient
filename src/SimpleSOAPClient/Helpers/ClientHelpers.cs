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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Handlers;

    /// <summary>
    /// Helper methods for working with <see cref="ISoapClient"/> instances.
    /// </summary>
    public static class ClientHelpers
    {
        #region Settings

        /// <summary>
        /// Sets the <see cref="SoapClientSettings"/> to be used by the client.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="settings">The settings to be used</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingSettings<TSoapClient>(
            this TSoapClient client, SoapClientSettings settings)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            client.Settings = settings;
            return client;
        }

        /// <summary>
        /// Sets the <see cref="SoapClientSettings.Default"/> as the settings to be used by the client.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient UsingDefaultSettings<TSoapClient>(this TSoapClient client)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            client.Settings = SoapClientSettings.Default;
            return client;
        }

        #endregion

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
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1Request"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Request<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnSoapEnvelopeV1Dot1RequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1RequestAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1Request"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Request<TSoapClient>(
            this TSoapClient client, Action<OnSoapEnvelopeV1Dot1RequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1RequestAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1RequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Request<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnSoapEnvelopeV1Dot1RequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1RequestAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1RequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Request<TSoapClient>(
            this TSoapClient client, Func<OnSoapEnvelopeV1Dot1RequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1RequestAsyncAction = async (c, args, ct) => await action(args, ct)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2Request"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Request<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnSoapEnvelopeV1Dot2RequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2RequestAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2Request"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Request<TSoapClient>(
            this TSoapClient client, Action<OnSoapEnvelopeV1Dot2RequestArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2RequestAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2RequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Request<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnSoapEnvelopeV1Dot2RequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2RequestAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2RequestAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Request<TSoapClient>(
            this TSoapClient client, Func<OnSoapEnvelopeV1Dot2RequestArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2RequestAsyncAction = async (c, args, ct) => await action(args, ct)
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
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1Response"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Response<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnSoapEnvelopeV1Dot1ResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1ResponseAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1Response"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Response<TSoapClient>(
            this TSoapClient client, Action<OnSoapEnvelopeV1Dot1ResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1ResponseAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1ResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Response<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnSoapEnvelopeV1Dot1ResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1ResponseAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot1ResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot1Response<TSoapClient>(
            this TSoapClient client, Func<OnSoapEnvelopeV1Dot1ResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot1ResponseAsyncAction = async (c, args, ct) => await action(args, ct)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2Response"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Response<TSoapClient>(
            this TSoapClient client, Action<ISoapClient, OnSoapEnvelopeV1Dot2ResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2ResponseAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2Response"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Response<TSoapClient>(
            this TSoapClient client, Action<OnSoapEnvelopeV1Dot2ResponseArguments> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2ResponseAction = (c, args) => action(args)
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2ResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Response<TSoapClient>(
            this TSoapClient client, Func<ISoapClient, OnSoapEnvelopeV1Dot2ResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2ResponseAsyncAction = action
            });
            return client;
        }

        /// <summary>
        /// Assigns the given delegate has an handler for <see cref="ISoapHandler.OnSoapEnvelopeV1Dot2ResponseAsync"/>
        /// operations using a <see cref="DelegatingSoapHandler"/> as a wrapper.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="client">The client to be used</param>
        /// <param name="action">The handler action</param>
        /// <param name="order">The handler order</param>
        /// <returns>The SOAP client after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TSoapClient OnSoapEnvelopeV1Dot2Response<TSoapClient>(
            this TSoapClient client, Func<OnSoapEnvelopeV1Dot2ResponseArguments, CancellationToken, Task> action, int order = 0)
            where TSoapClient : ISoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (action == null) throw new ArgumentNullException(nameof(action));

            client.AddHandler(new DelegatingSoapHandler
            {
                Order = order,
                OnSoapEnvelopeV1Dot2ResponseAsyncAction = async (c, args, ct) => await action(args, ct)
            });
            return client;
        }

        #endregion

        #endregion

        #region HttpClient

        /// <summary>
        /// Allows an handler to configure the <see cref="SoapClient.HttpClient"/> instance.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP Client type</typeparam>
        /// <param name="client">The client to configure</param>
        /// <param name="cfgHandler">The configuration handler</param>
        /// <returns>The client after changes</returns>
        public static TSoapClient UsingClientConfiguration<TSoapClient>(
            this TSoapClient client, Action<HttpClient> cfgHandler)
            where TSoapClient : SoapClient
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (cfgHandler == null) throw new ArgumentNullException(nameof(cfgHandler));

            cfgHandler(client.HttpClient);

            return client;
        }

        #endregion
    }
}

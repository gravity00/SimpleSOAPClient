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

    /// <summary>
    /// Helper methods for working with <see cref="ISoapClientFactory"/> instances.
    /// </summary>
    public static class ClientFactoryHelpers
    {
        #region Sync

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void GetAndRelease<TSoapClient>(this ISoapClientFactory factory, Action<TSoapClient> action)
            where TSoapClient : ISoapClient
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var client = factory.Get<TSoapClient>();
            try
            {
                action(client);
            }
            finally
            {
                factory.Release(client);
            }
        }

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void GetAndRelease(this ISoapClientFactory factory, Action<SoapClient> action)
        {
            GetAndRelease<SoapClient>(factory, action);
        }

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <returns>The action result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TResult GetAndRelease<TSoapClient, TResult>(this ISoapClientFactory factory, Func<TSoapClient, TResult> action)
            where TSoapClient : ISoapClient
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var client = factory.Get<TSoapClient>();
            try
            {
                return action(client);
            }
            finally
            {
                factory.Release(client);
            }
        }

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <returns>The action result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TResult GetAndRelease<TResult>(this ISoapClientFactory factory, Func<SoapClient, TResult> action)
        {
            return GetAndRelease<SoapClient, TResult>(factory, action);
        }

        #endregion

        #region Async

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <param name="ct">The cancelattion token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task GetAndReleaseAsync<TSoapClient>(
            this ISoapClientFactory factory, Func<TSoapClient, CancellationToken, Task> action, CancellationToken ct = default(CancellationToken))
            where TSoapClient : ISoapClient
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var client = factory.Get<TSoapClient>();
            try
            {
                await action(client, ct);
            }
            finally
            {
                factory.Release(client);
            }
        }

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <param name="ct">The cancelattion token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task GetAndReleaseAsync(
            this ISoapClientFactory factory, Func<SoapClient, CancellationToken, Task> action, CancellationToken ct = default(CancellationToken))
        {
            await GetAndReleaseAsync<SoapClient>(factory, action, ct);
        }

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <typeparam name="TSoapClient">The SOAP client type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<TResult> GetAndReleaseAsync<TSoapClient, TResult>(
            this ISoapClientFactory factory, Func<TSoapClient, CancellationToken, Task<TResult>> action, CancellationToken ct = default(CancellationToken))
            where TSoapClient : ISoapClient
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var client = factory.Get<TSoapClient>();
            try
            {
                return await action(client, ct);
            }
            finally
            {
                factory.Release(client);
            }
        }

        /// <summary>
        /// Gets a <see cref="ISoapClient"/> instance from the factory and releases 
        /// when the action completes.
        /// </summary>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="factory">The factory to use</param>
        /// <param name="action">The action to execute</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<TResult> GetAndReleaseAsync<TResult>(
            this ISoapClientFactory factory, Func<SoapClient, CancellationToken, Task<TResult>> action, CancellationToken ct = default(CancellationToken))
        {
            return await GetAndReleaseAsync<SoapClient, TResult>(factory, action, ct);
        }

        #endregion
    }
}

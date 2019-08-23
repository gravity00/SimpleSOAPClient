using System;

// ReSharper disable once CheckNamespace
namespace SimpleSOAPClient
{
    /// <summary>
    /// Extensions for <see cref="IHaveSoapRequestSettings"/>.
    /// </summary>
    public static class HaveSoapRequestSettingsExtensions
    {
        /// <summary>
        /// Configures the request timeout
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestSettings"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static T Timeout<T>(this T requestSettings, TimeSpan timeout)
            where T : IHaveSoapRequestSettings
        {
            requestSettings.Settings.Timeout = timeout;
            return requestSettings;
        }

        /// <summary>
        /// Configures the request timeout in milliseconds. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestSettings"></param>
        /// <param name="msTimeout"></param>
        /// <returns></returns>
        public static T Timeout<T>(this T requestSettings, int msTimeout)
            where T : IHaveSoapRequestSettings
        {
            requestSettings.Settings.Timeout = TimeSpan.FromMilliseconds(msTimeout);
            return requestSettings;
        }

        /// <summary>
        /// Configures the SOAP protocol. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestSettings"></param>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public static T Protocol<T>(this T requestSettings, SoapProtocol protocol)
            where T : IHaveSoapRequestSettings
        {
            requestSettings.Settings.Protocol = protocol;
            return requestSettings;
        }

        /// <summary>
        /// Configures the endpoint address
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestSettings"></param>
        /// <param name="endpointAddress"></param>
        /// <returns></returns>
        public static T EndpointAddress<T>(this T requestSettings, Uri endpointAddress)
            where T : IHaveSoapRequestSettings
        {
            requestSettings.Settings.EndpointAddress = endpointAddress;
            return requestSettings;
        }

        /// <summary>
        /// Configures the endpoint address
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestSettings"></param>
        /// <param name="endpointAddress"></param>
        /// <returns></returns>
        public static T EndpointAddress<T>(this T requestSettings, string endpointAddress)
            where T : IHaveSoapRequestSettings
        {
            requestSettings.Settings.EndpointAddress = new Uri(endpointAddress, UriKind.Absolute);
            return requestSettings;
        }
    }
}

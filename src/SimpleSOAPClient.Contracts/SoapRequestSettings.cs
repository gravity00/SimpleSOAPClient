using System;

namespace SimpleSOAPClient
{
    /// <summary>
    /// SOAP request settings
    /// </summary>
    public struct SoapRequestSettings
    {
        /// <summary>
        /// Default SOAP protocol version.
        /// </summary>
        public const SoapProtocol DefaultProtocol = SoapProtocol.Version11;

        /// <summary>
        /// Default request timeout (00:00:15).
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);

        private Uri _endpointAddress;
        private SoapProtocol _protocol;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="endpointAddress"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public SoapRequestSettings(Uri endpointAddress)
        {
            AssertEndpointAddress(endpointAddress, nameof(endpointAddress));

            _endpointAddress = endpointAddress;
            Timeout = DefaultTimeout;
            _protocol = DefaultProtocol;
        }

        /// <summary>
        /// The endpoint the request will be sent
        /// </summary>
        public Uri EndpointAddress
        {
            get => _endpointAddress;
            set
            {
                AssertEndpointAddress(value, nameof(value));
                _endpointAddress = value;
            }
        }

        /// <summary>
        /// Request timeout
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// SOAP protocol version. Defaults to <see cref="DefaultProtocol"/>.
        /// </summary>
        public SoapProtocol Protocol
        {
            get => _protocol;
            set
            {
                if (value != SoapProtocol.Version11 && value != SoapProtocol.Version12)
                    throw new ArgumentException($"Unknown protocol version: {value}", nameof(value));
                _protocol = value;
            }
        }

        private static void AssertEndpointAddress(Uri endpointAddress, string argName)
        {
            if (endpointAddress == null)
                throw new ArgumentNullException(argName);
            if (!endpointAddress.IsAbsoluteUri)
                throw new ArgumentException("Endpoint address must be an absolute Uri", argName);
        }
    }
}
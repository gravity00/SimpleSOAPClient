using System;

namespace SimpleSOAPClient
{
    /// <summary>
    /// SOAP request settings
    /// </summary>
    public class SoapRequestSettings
    {
        /// <summary>
        /// Default SOAP protocol version.
        /// </summary>
        public const SoapProtocol DefaultProtocol = SoapProtocol.Version11;

        /// <summary>
        /// Default request timeout (00:00:15).
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);

        private SoapProtocol? _protocol;

        /// <summary>
        /// Request timeout
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// SOAP protocol version. Defaults to <see cref="DefaultProtocol"/>.
        /// </summary>
        public SoapProtocol? Protocol
        {
            get => _protocol;
            set
            {
                if (value == null)
                    _protocol = null;
                else
                {
                    if (value != SoapProtocol.Version11 && value != SoapProtocol.Version12)
                        throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown protocol version");
                    _protocol = value;
                }
            }
        }
    }
}
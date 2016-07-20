namespace SimpleSOAPClient.Handlers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// The SOAP Handler arguments for <see cref="ISoapHandler.BeforeHttpRequest"/> method.
    /// </summary>
    public sealed class BeforeHttpRequestArguments : SoapHandlerArguments
    {
        private HttpRequestMessage _request;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="request">The HTTP message request</param>
        /// <param name="url">The SOAP service url</param>
        /// <param name="action">The SOAP action</param>
        /// <param name="trackingId">An optional tracking id</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public BeforeHttpRequestArguments(HttpRequestMessage request, string url, string action, Guid? trackingId = null) 
            : base(url, action, trackingId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _request = request;
        }

        #region Implementation of IBeforeHttpRequestArguments

        /// <summary>
        /// The current HTTP request message
        /// </summary>
        public HttpRequestMessage Request
        {
            get { return _request; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _request = value;
            }
        }

        #endregion
    }
}
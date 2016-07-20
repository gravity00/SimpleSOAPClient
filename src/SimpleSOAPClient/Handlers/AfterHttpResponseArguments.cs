namespace SimpleSOAPClient.Handlers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// The SOAP Handler arguments for <see cref="ISoapHandler.AfterHttpResponse"/> method.
    /// </summary>
    public sealed class AfterHttpResponseArguments : SoapHandlerArguments
    {
        private HttpResponseMessage _response;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="response">The HTTP message response</param>
        /// <param name="url">The SOAP service url</param>
        /// <param name="action">The SOAP action</param>
        /// <param name="trackingId">An optional tracking id</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public AfterHttpResponseArguments(HttpResponseMessage response, string url, string action, Guid? trackingId = null) 
            : base(url, action, trackingId)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            _response = response;
        }

        #region Implementation of IAfterHttpResponseArguments

        /// <summary>
        /// The current HTTP response message
        /// </summary>
        public HttpResponseMessage Response
        {
            get { return _response; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _response = value;
            }
        }

        #endregion
    }
}
namespace SimpleSOAPClient.Handlers
{
    using System;

    /// <summary>
    /// Base class for SOAP Handler argument classes
    /// </summary>
    public abstract class SoapHandlerArguments
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The SOAP service url</param>
        /// <param name="action">The SOAP action</param>
        /// <param name="trackingId">An optional tracking id</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected SoapHandlerArguments(string url, string action, Guid? trackingId = null)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));
            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(action));

            Url = url;
            Action = action;
            TrackingId = trackingId ?? Guid.NewGuid();
        }

        #region Implementation of ISoapHandlerArguments

        /// <summary>
        /// The URL being invoked
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The action being invoked
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// A unique identifier to track the current request
        /// </summary>
        public Guid TrackingId { get; }

        /// <summary>
        /// An object state that will be passed
        /// accross all the handlers
        /// </summary>
        public object State { get; set; }

        #endregion
    }
}
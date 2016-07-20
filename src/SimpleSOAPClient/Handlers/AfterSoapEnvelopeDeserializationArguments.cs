namespace SimpleSOAPClient.Handlers
{
    using System;
    using Models;

    /// <summary>
    /// The SOAP Handler arguments for <see cref="ISoapHandler.AfterSoapEnvelopeDeserialization"/> method.
    /// </summary>
    public sealed class AfterSoapEnvelopeDeserializationArguments : SoapHandlerArguments
    {
        private SoapEnvelope _envelope;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="envelope">The SOAP envelope</param>
        /// <param name="url">The SOAP service url</param>
        /// <param name="action">The SOAP action</param>
        /// <param name="trackingId">An optional tracking id</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public AfterSoapEnvelopeDeserializationArguments(SoapEnvelope envelope, string url, string action, Guid? trackingId = null) 
            : base(url, action, trackingId)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            _envelope = envelope;
        }

        #region Implementation of IAfterSoapEnvelopeDeserializationArguments

        /// <summary>
        /// The SOAP Envelope that was deserialized
        /// </summary>
        public SoapEnvelope Envelope
        {
            get { return _envelope; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _envelope = value;
            }
        }

        #endregion
    }
}
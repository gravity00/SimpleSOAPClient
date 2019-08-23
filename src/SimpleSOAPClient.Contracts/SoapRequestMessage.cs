namespace SimpleSOAPClient
{
    /// <summary>
    /// The SOAP request message
    /// </summary>
    public sealed class SoapRequestMessage
    {
        private SoapRequestSettings _settings;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="body"></param>
        /// <param name="action"></param>
        public SoapRequestMessage(object body, string action = "")
        {
            Action = action;
            Envelope = new SoapEnvelopeRequest(body);
        }

        /// <summary>
        /// Request settings
        /// </summary>
        public SoapRequestSettings Settings => _settings ?? (_settings = new SoapRequestSettings());

        /// <summary>
        /// The SOAP action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The SOAP envelope
        /// </summary>
        public SoapEnvelopeRequest Envelope { get; }
    }
}
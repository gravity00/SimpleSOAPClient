using System;

namespace SimpleSOAPClient
{
    /// <summary>
    /// Represents a SOAP envelope
    /// </summary>
    /// <typeparam name="THeader"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public abstract class SoapEnvelope<THeader, TContent> 
        where THeader : class 
        where TContent : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="content">The body content</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected SoapEnvelope(TContent content)
        {
            Header = new SoapEnvelopeHeader<THeader>();
            Body = new SoapEnvelopeBody<TContent>(content);
        }

        /// <summary>
        /// Encoding style
        /// </summary>
        public string EncodingStyle { get; set; }

        /// <summary>
        /// The SOAP envelope headers
        /// </summary>
        public SoapEnvelopeHeader<THeader> Header { get; }

        /// <summary>
        /// The SOAP envelope body
        /// </summary>
        public SoapEnvelopeBody<TContent> Body { get; }
    }
}
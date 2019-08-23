using System.Collections.Generic;

namespace SimpleSOAPClient
{
    /// <summary>
    /// Represents a collection of SOAP headers
    /// </summary>
    /// <typeparam name="THeader"></typeparam>
    public class SoapEnvelopeHeader<THeader> where THeader : class
    {
        /// <summary>
        /// Encoding style
        /// </summary>
        public string EncodingStyle { get; set; }

        /// <summary>
        /// Collection of SOAP headers
        /// </summary>
        public List<THeader> Headers { get; } = new List<THeader>();
    }
}
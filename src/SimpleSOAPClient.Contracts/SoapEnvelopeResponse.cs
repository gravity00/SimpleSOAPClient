using System;
using System.Xml.Linq;

namespace SimpleSOAPClient
{
    /// <summary>
    /// Represents a SOAP envelope response
    /// </summary>
    public class SoapEnvelopeResponse : SoapEnvelope<XElement, XElement>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="content">The body content</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SoapEnvelopeResponse(XElement content) : base(content)
        {

        }
    }
}
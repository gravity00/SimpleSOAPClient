namespace SimpleSOAPClient.Models
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents the SOAP Envelope Header section
    /// </summary>
    public class SoapEnvelopeHeader
    {
        /// <summary>
        /// The collection of headers
        /// </summary>
        [XmlAnyElement]
        public XElement[] Headers { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SoapEnvelopeHeader"/>
        /// </summary>
        public SoapEnvelopeHeader()
        {
            Headers = new XElement[0];
        }
    }
}
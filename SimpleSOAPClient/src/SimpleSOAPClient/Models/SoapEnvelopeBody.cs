namespace SimpleSOAPClient.Models
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents the SOAP Envelope Body section
    /// </summary>
    public class SoapEnvelopeBody
    {
        /// <summary>
        /// The body content
        /// </summary>
        [XmlAnyElement]
        public XElement Value { get; set; }
    }
}

namespace SimpleSOAPClient.Models
{
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a SOAP Envelope
    /// </summary>
    [XmlRoot("Envelope", Namespace = Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope)]
    public class SoapEnvelope
    {
        /// <summary>
        /// The SOAP Envelope Header section
        /// </summary>
        [XmlElement("Header")]
        public SoapEnvelopeHeader Header { get; set; }

        /// <summary>
        /// The SOAP Envelope Body section
        /// </summary>
        [XmlElement("Body")]
        public SoapEnvelopeBody Body { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SoapEnvelope"/>
        /// </summary>
        public SoapEnvelope()
        {
            Header = new SoapEnvelopeHeader();
            Body = new SoapEnvelopeBody();
        }

        /// <summary>
        /// Prepares a new SOAP Envelope to be manipulated
        /// </summary>
        /// <returns>The <see cref="SoapEnvelope"/> instance</returns>
        public static SoapEnvelope Prepare()
        {
            return new SoapEnvelope();
        }
    }
}
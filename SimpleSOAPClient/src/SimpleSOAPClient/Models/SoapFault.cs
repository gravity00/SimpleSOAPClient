namespace SimpleSOAPClient.Models
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a SOAP Fault
    /// </summary>
    [XmlRoot("Fault", Namespace = Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope)]
    public class SoapFault
    {
        /// <summary>
        /// The fault code
        /// </summary>
        [XmlElement("faultcode", Namespace = "")]
        public string Code { get; set; }

        /// <summary>
        /// The fault string
        /// </summary>
        [XmlElement("faultstring", Namespace = "")]
        public string String { get; set; }

        /// <summary>
        /// The fault actor
        /// </summary>
        [XmlElement("faultactor", Namespace = "")]
        public string Actor { get; set; }

        /// <summary>
        /// The fault detail
        /// </summary>
        [XmlAnyElement("detail", Namespace = "")]
        public XElement Detail { get; set; }
    }
}
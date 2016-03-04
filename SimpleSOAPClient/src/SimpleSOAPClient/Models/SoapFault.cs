namespace SimpleSOAPClient.Models
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    [XmlRoot("Fault", Namespace = Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope)]
    public class SoapFault
    {
        [XmlElement("faultcode", Namespace = "")]
        public string FaultCode { get; set; }

        [XmlElement("faultstring", Namespace = "")]
        public string FaultString { get; set; }

        [XmlElement("faultactor", Namespace = "")]
        public string FaultActor { get; set; }

        [XmlAnyElement("detail", Namespace = "")]
        public XElement Detail { get; set; }
    }
}
namespace SimpleSOAPClient.Models
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public class SoapEnvelopeBody
    {
        [XmlAnyElement]
        public XElement Value { get; set; }
    }
}

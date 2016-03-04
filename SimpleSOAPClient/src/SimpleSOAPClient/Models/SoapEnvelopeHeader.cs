namespace SimpleSOAPClient.Models
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public class SoapEnvelopeHeader
    {
        [XmlAnyElement]
        public XElement[] Headers { get; set; }

        public SoapEnvelopeHeader()
        {
            Headers = new XElement[0];
        }
    }
}
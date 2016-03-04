namespace SimpleSOAPClient.Models
{
    using System.Xml.Serialization;

    [XmlRoot("Envelope", Namespace = Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope)]
    public class SoapEnvelope
    {
        [XmlElement("Header")]
        public SoapEnvelopeHeader Header { get; set; }

        [XmlElement("Body")]
        public SoapEnvelopeBody Body { get; set; }

        public SoapEnvelope()
        {
            Header = new SoapEnvelopeHeader();
            Body = new SoapEnvelopeBody();
        }

        public static SoapEnvelope Prepare()
        {
            return new SoapEnvelope();
        }
    }
}
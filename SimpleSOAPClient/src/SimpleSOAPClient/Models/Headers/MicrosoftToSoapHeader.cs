namespace SimpleSOAPClient.Models.Headers
{
    using System.Xml.Serialization;

    [XmlRoot("To", Namespace = Constant.Namespace.ComMicrosoftSchemasWs200505AddressingNone)]
    public class MicrosoftToSoapHeader : SoapHeader
    {
        [XmlText]
        public string To { get; set; }
    }
}

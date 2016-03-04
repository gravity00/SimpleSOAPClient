namespace SimpleSOAPClient.Models.Headers
{
    using System.Xml.Serialization;

    [XmlRoot("Action", Namespace = Constant.Namespace.ComMicrosoftSchemasWs200505AddressingNone)]
    public class MicrosoftActionSoapHeader : SoapHeader
    {
        [XmlText]
        public string Action { get; set; }
    }
}
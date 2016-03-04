namespace SimpleSOAPClient.Models.Headers
{
    using System.Xml.Serialization;

    /// <summary>
    /// The Microsoft To SOAP Header
    /// </summary>
    [XmlRoot("To", Namespace = Constant.Namespace.ComMicrosoftSchemasWs200505AddressingNone)]
    public class MicrosoftToSoapHeader : SoapHeader
    {
        /// <summary>
        /// The header to content
        /// </summary>
        [XmlText]
        public string To { get; set; }
    }
}

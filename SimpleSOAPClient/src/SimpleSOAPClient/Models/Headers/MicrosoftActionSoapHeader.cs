namespace SimpleSOAPClient.Models.Headers
{
    using System.Xml.Serialization;

    /// <summary>
    /// The Microsoft Action SOAP Header
    /// </summary>
    [XmlRoot("Action", Namespace = Constant.Namespace.ComMicrosoftSchemasWs200505AddressingNone)]
    public class MicrosoftActionSoapHeader : SoapHeader
    {
        /// <summary>
        /// The header action content
        /// </summary>
        [XmlText]
        public string Action { get; set; }
    }
}
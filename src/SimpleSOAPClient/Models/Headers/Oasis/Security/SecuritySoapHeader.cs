namespace SimpleSOAPClient.Models.Headers.Oasis.Security
{
    using System.Xml.Serialization;

    /// <summary>
    /// Represents the SOAP Security Header
    /// </summary>
    [XmlRoot("Security",
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class SecuritySoapHeader : SoapHeaderOld
    {
        /// <summary>
        /// The header timestamp
        /// </summary>
        [XmlElement("Timestamp",
            Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecurityUtility10)]
        public Timestamp Timestamp { get; set; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SecuritySoapHeader()
        {
            MustUnderstand = 1;
        }
    }
}
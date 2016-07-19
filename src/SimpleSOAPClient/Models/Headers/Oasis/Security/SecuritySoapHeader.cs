namespace SimpleSOAPClient.Models.Headers.Oasis.Security
{
    using System.Xml.Serialization;

    [XmlRoot("Security",
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class SecuritySoapHeader : SoapHeader
    {
        [XmlElement("Timestamp",
            Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecurityUtility10)]
        public Timestamp Timestamp { get; set; }

        public SecuritySoapHeader()
        {
            MustUnderstand = 1;
        }
    }
}
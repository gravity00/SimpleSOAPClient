namespace SimpleSOAPClient.Models.Headers
{
    using System.Xml.Serialization;

    [XmlRoot("Security", 
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class OasisSecurityWithUsernameTokenAndPasswordText : SoapHeader
    {
        [XmlElement("Timestamp")]
        public SoapSecurityTimestamp Timestamp { get; set; }

        [XmlElement("UsernameToken")]
        public OasisSecurityUsernameTokenWithPasswordText UsernameToken { get; set; }

        public OasisSecurityWithUsernameTokenAndPasswordText()
        {
            MustUnderstand = 1;
        }
    }
}

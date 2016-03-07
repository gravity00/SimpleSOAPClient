namespace SimpleSOAPClient.Models.Headers.Oasis.Security
{
    using System.Xml.Serialization;

    [XmlRoot("Security", 
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class UsernameTokenAndPasswordTextSoapHeader : SoapHeader
    {
        [XmlElement("Timestamp")]
        public Timestamp Timestamp { get; set; }

        [XmlElement("UsernameToken")]
        public UsernameTokenWithPasswordText UsernameToken { get; set; }

        public UsernameTokenAndPasswordTextSoapHeader()
        {
            MustUnderstand = 1;
        }
    }
}

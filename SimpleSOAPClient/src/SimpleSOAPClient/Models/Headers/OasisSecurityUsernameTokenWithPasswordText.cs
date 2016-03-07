namespace SimpleSOAPClient.Models.Headers
{
    using System.Xml.Serialization;

    [XmlType("UsernameToken", 
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class OasisSecurityUsernameTokenWithPasswordText
    {
        [XmlAttribute("Id", 
            Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecurityUtility10)]
        public string Id { get; set; }

        [XmlElement("Username")]
        public string Username { get; set; }

        [XmlElement("Password", 
            DataType = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssUsernameTokenProfile10PasswordText)]
        public string Password { get; set; }
    }
}
namespace SimpleSOAPClient.Models.Headers.Oasis.Security
{
    using System.Xml.Serialization;

    [XmlType("UsernameToken", 
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class UsernameTokenWithPasswordText
    {
        [XmlAttribute("Id", 
            Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecurityUtility10)]
        public string Id { get; set; }

        [XmlElement("Username")]
        public string Username { get; set; }

        [XmlElement("Password")]
        public UsernameTokenPasswordText Password { get; set; }
    }
}
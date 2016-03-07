namespace SimpleSOAPClient.Models.Headers.Oasis.Security
{
    using System.Xml.Serialization;

    [XmlType("Password",
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class UsernameTokenPasswordText
    {
        [XmlAttribute("Type", Namespace = "")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }

        public UsernameTokenPasswordText()
        {
            Type = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssUsernameTokenProfile10PasswordText;
        }
    }
}
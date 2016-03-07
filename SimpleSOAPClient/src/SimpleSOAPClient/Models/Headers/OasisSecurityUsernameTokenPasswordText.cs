namespace SimpleSOAPClient.Models.Headers
{
    using System.Xml.Serialization;

    [XmlType("Password",
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class OasisSecurityUsernameTokenPasswordText
    {
        [XmlAttribute("Type", Namespace = "")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
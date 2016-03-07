namespace SimpleSOAPClient.Models.Headers
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Timestamp", 
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecurityUtility10)]
    public class SoapSecurityTimestamp
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement("Timestamp")]
        public DateTime Created { get; set; }

        [XmlElement("Expires")]
        public DateTime Expires { get; set; }
    }
}
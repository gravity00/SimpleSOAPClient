using System.Xml.Serialization;

namespace SimpleSOAPClient.Models.Xop
{
    /// <summary>
    /// A reference to an attachment to the SoapEnvelope; see https://www.w3.org/TR/soap12-mtom/
    /// </summary>
    [XmlType(Namespace = "http://www.w3.org/2004/08/xop/include")]
    public class Include
    {
        /// <summary>
        /// The Content Id of the associated attachment.
        /// </summary>
        [XmlIgnore]
        public string ContentId { get; set; }

        /// <summary>
        /// Helper method to generate the href attribute when serialized to XML.
        /// </summary>
        [XmlAttribute(AttributeName = "href")]
        public string ContentIdAsHref => "cid:" + ContentId;

    }
}
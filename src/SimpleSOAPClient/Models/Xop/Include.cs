using System;
using System.Xml.Serialization;

namespace SimpleSOAPClient.Models.Xop
{
    
    /// <summary>
    /// A reference to an attachment to the SoapEnvelope; see https://www.w3.org/TR/soap12-mtom/
    /// </summary>
    [XmlType(Namespace = Namespace)]
    public class Include
    {
        /// <summary>
        /// The W3 namespace for XOP Include
        /// </summary>
        public const string Namespace = "http://www.w3.org/2004/08/xop/include";
        
        /// <summary>
        /// The Content Id of the associated attachment.
        /// </summary>
        [XmlIgnore]
        public string ContentId { get; set; }

        /// <summary>
        /// Helper method to generate the href attribute when serialized to XML.
        /// </summary>
        [XmlAttribute(AttributeName = "href")]
        public string ContentIdAsHref
        {
            get => "cid:" + ContentId;
            set
            {
                if (!value.StartsWith("cid:", StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Content Id must start 'cid:'.");
                }
                ContentId = value.Substring(4, value.Length - 4);
            }
        }

    }
}
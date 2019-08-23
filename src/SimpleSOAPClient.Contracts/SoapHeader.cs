using System.Xml.Serialization;

namespace SimpleSOAPClient
{
    /// <summary>
    /// Represents a SOAP header
    /// </summary>
    public abstract class SoapHeader
    {
        /// <summary>
        /// Encoding style
        /// </summary>
        [XmlIgnore]
        public string EncodingStyle { get; set; }

        /// <summary>
        /// Header actor
        /// </summary>
        [XmlIgnore]
        public string Actor { get; set; }

        /// <summary>
        /// Header must understant
        /// </summary>
        [XmlIgnore]
        public bool MustUnderstand { get; set; }
    }
}
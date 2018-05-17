using System.Xml.Serialization;

namespace SimpleSOAPClient.Tests
{
    [XmlRoot(Namespace = "urn:simplesoapclient:test")]
    public class XmlModel
    {
        [XmlElement(Order = 0)]
        public string String { get; set; }

        [XmlElement(Order = 1)]
        public int Int { get; set; }

        [XmlElement(Order = 2)]
        public bool Bool { get; set; }

        [XmlArray(Order = 3)]
        public string[] Array { get; set; }
    }
}

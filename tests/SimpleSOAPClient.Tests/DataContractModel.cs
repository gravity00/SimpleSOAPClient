using System.Runtime.Serialization;

namespace SimpleSOAPClient.Tests
{
    [DataContract(Name = "DataContractModel", Namespace ="urn:simplesoapclient:test")]
    public class DataContractModel
    {
        [DataMember(Order = 0)]
        public string String { get; set; }

        [DataMember(Order = 1)]
        public int Int { get; set; }

        [DataMember(Order = 2)]
        public bool Bool { get; set; }

        [DataMember(Order = 3)]
        public string[] Array { get; set; }
    }
}

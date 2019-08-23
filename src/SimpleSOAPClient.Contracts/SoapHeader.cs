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
        public string EncodingStyle { get; set; }

        /// <summary>
        /// Header actor
        /// </summary>
        public string Actor { get; set; }

        /// <summary>
        /// Header must understant
        /// </summary>
        public bool MustUnderstand { get; set; }
    }
}
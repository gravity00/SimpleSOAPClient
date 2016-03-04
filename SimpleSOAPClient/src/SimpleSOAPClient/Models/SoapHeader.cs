namespace SimpleSOAPClient.Models
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Base classe for SOAP Headers
    /// </summary>
    public abstract class SoapHeader
    {
        private int _mustUnderstand;

        /// <summary>
        /// Does the header must be understand?
        /// </summary>
        [XmlAttribute("mustUnderstand", Namespace = Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope)]
        public int MustUnderstand
        {
            get { return _mustUnderstand; }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Must understand must be 0 or 1");
                _mustUnderstand = value;
            }
        }
    }
}

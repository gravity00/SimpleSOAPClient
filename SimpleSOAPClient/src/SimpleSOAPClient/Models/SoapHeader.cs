namespace SimpleSOAPClient.Models
{
    using System;
    using System.Xml.Serialization;

    public abstract class SoapHeader
    {
        private int _mustUnderstand;

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

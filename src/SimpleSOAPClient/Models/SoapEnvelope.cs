#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System.Collections.Generic;
using System.Net.Http;

namespace SimpleSOAPClient.Models
{
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a SOAP Envelope
    /// </summary>
    [XmlRoot("Envelope", Namespace = Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope)]
    public class SoapEnvelope
    {
        /// <summary>
        /// The SOAP Envelope Header section
        /// </summary>
        [XmlElement("Header")]
        public SoapEnvelopeHeader Header { get; set; }

        /// <summary>
        /// The SOAP Envelope Body section
        /// </summary>
        [XmlElement("Body")]
        public SoapEnvelopeBody Body { get; set; }
        
        /// <summary>
        /// MTOM attachments of the SOAP message.
        /// See https://www.w3.org/TR/soap12-mtom.
        /// </summary>
        [XmlIgnore]
        public IDictionary<string, HttpContent> Attachments { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SoapEnvelope"/>
        /// </summary>
        public SoapEnvelope()
        {
            Header = new SoapEnvelopeHeader();
            Body = new SoapEnvelopeBody();
        }

        /// <summary>
        /// Prepares a new SOAP Envelope to be manipulated
        /// </summary>
        /// <returns>The <see cref="SoapEnvelope"/> instance</returns>
        public static SoapEnvelope Prepare()
        {
            return new SoapEnvelope();
        }
    }
}
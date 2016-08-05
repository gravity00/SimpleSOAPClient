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
namespace SimpleSOAPClient.Models.V1_2
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a SOAP version 1.2 Fault
    /// </summary>
    [XmlRoot("Fault", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
    public class SoapFault
    {
        /// <summary>
        /// The fault code
        /// </summary>
        [XmlElement("Code", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public SoapFaultCode Code { get; set; }

        /// <summary>
        /// The fault reason
        /// </summary>
        [XmlElement("Reason", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public SoapFaultReason Reason { get; set; }

        /// <summary>
        /// The fault node
        /// </summary>
        [XmlElement("Node", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public string Node { get; set; }

        /// <summary>
        /// The fault node
        /// </summary>
        [XmlElement("Role", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public string Role { get; set; }

        /// <summary>
        /// The fault detail
        /// </summary>
        [XmlElement("Detail", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public XElement Detail { get; set; }
    }
}

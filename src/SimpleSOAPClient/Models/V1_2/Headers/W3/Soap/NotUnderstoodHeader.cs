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
namespace SimpleSOAPClient.Models.V1_2.Headers.W3.Soap
{
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// The not understood header thrown by MustUnderstand faults
    /// </summary>
    [XmlRoot("NotUnderstood", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
    public class NotUnderstoodHeader : SoapEnvelopeHeaderBlock
    {
        /// <summary>
        /// The QName for the not understood header
        /// </summary>
        [XmlAttribute("qname", Namespace = "")]
        public string QName { get; set; }

        /// <summary>
        /// The namespaces declarations
        /// </summary>
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns { get; set; }

        /// <summary>
        /// Tries to get the supported envelope namespace based 
        /// on the <see cref="QName"/> prefix
        /// </summary>
        /// <param name="ns">The not understood header namespace or null if not found</param>
        /// <returns>True if a match namespace is found for the QName prefix</returns>
        public bool TryGetNotUnderstoodNamespace(out XmlQualifiedName ns)
        {
            var idx = QName.IndexOf(':');
            if (idx > 0)
            {
                var nsName = QName.Substring(0, idx);
                ns = Xmlns.ToArray().FirstOrDefault(e => e.Name == nsName);
                return ns != null;
            }

            ns = null;
            return false;
        }
    }
}

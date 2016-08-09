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
    using System.Xml.Serialization;

    /// <summary>
    /// The upgrade header thrown by VersionMismatch faults
    /// </summary>
    [XmlRoot("Upgrade", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
    public class UpgradeHeader : SoapEnvelopeHeaderBlock
    {
        /// <summary>
        /// The supported envelopes
        /// </summary>
        [XmlElement("SupportedEnvelope", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public UpgradeHeaderSupportedEnvelope[] SupportedEnvelopes { get; set; }
    }
}

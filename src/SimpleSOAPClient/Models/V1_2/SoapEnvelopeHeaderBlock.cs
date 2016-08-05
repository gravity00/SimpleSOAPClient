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
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a SOAP Header 
    /// </summary>
    public abstract class SoapEnvelopeHeaderBlock
    {
        /// <summary>
        /// The encoding style attribute
        /// </summary>
        [XmlAttribute("encodingStyle", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public string EncodingStyle { get; set; }

        /// <summary>
        /// The role attribute
        /// </summary>
        [XmlAttribute("role", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public string Role { get; set; }

        /// <summary>
        /// The must understand attribute
        /// </summary>
        [XmlIgnore]
        public bool? MustUnderstand { get; set; }

        /// <summary>
        /// The must understand attribute value used for XML serialization.
        /// To assign this value, consider using <see cref="MustUnderstand"/> property.
        /// </summary>
        [XmlAttribute("mustUnderstand", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public string MustUnderstandValue
        {
            get
            {
                return MustUnderstand == null || !MustUnderstand.Value ? null : MustUnderstand.ToString().ToLowerInvariant();
            }
            set
            {
                if (value == null)
                    MustUnderstand = null;
                else
                {
                    var cleanValue = value.Trim().ToLowerInvariant();
                    switch (cleanValue)
                    {
                        case "1":
                        case "true":
                            MustUnderstand = true;
                            break;
                        case "0":
                        case "false":
                            MustUnderstand = false;
                            break;
                        default:
                            throw new FormatException(
                                $"Invalid MustUnderstand value format. Found '{cleanValue}' but should be 'true', 'false', '1' or '0'");
                    }
                }
            }
        }

        /// <summary>
        /// The relay attribute
        /// </summary>
        [XmlIgnore]
        public bool? Relay { get; set; }

        /// <summary>
        /// The relay attribute value used for XML serialization.
        /// To assign this value, consider using <see cref="Relay"/> property.
        /// </summary>
        [XmlAttribute("relay", Namespace = Constant.Namespace.OrgW3Www200305SoapEnvelope)]
        public string RelayValue
        {
            get
            {
                return Relay == null || !Relay.Value ? null : Relay.ToString().ToLowerInvariant();
            }
            set
            {
                if (value == null)
                    Relay = null;
                else
                {
                    var cleanValue = value.Trim().ToLowerInvariant();
                    switch (cleanValue)
                    {
                        case "1":
                        case "true":
                            Relay = true;
                            break;
                        case "0":
                        case "false":
                            Relay = false;
                            break;
                        default:
                            throw new FormatException(
                                $"Invalid Relay value format. Found '{cleanValue}' but should be 'true', 'false', '1' or '0'");
                    }
                }
            }
        }
    }
}

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
namespace SimpleSOAPClient
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Exceptions;
    using Models;

    /// <summary>
    /// Provider for serialization and deserialization of <see cref="SoapEnvelopeOld"/> instances.
    /// </summary>
    public class SoapEnvelopeSerializationProvider : ISoapEnvelopeSerializationProvider
    {
        private XmlWriterSettings _xmlWriterSettings;
        private XmlSerializerNamespaces _xmlSerializerNamespaces;

        /// <summary>
        /// XML writer settings to be used when serializing <see cref="SoapEnvelopeOld"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public XmlWriterSettings XmlWriterSettings
        {
            get { return _xmlWriterSettings; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _xmlWriterSettings = value;
            }
        }

        /// <summary>
        /// XML serializer namespaces to be used when serializing <see cref="SoapEnvelopeOld"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public XmlSerializerNamespaces XmlSerializerNamespaces
        {
            get { return _xmlSerializerNamespaces; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _xmlSerializerNamespaces = value;
            }
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SoapEnvelopeSerializationProvider()
        {
            _xmlWriterSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = false,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };

            _xmlSerializerNamespaces = new XmlSerializerNamespaces();
            _xmlSerializerNamespaces.Add("", "");
        }

        #region Implementation of ISoapEnvelopeSerializationProvider

        /// <summary>
        /// Serializes a given <see cref="SoapEnvelopeOld"/> instance into a XML string.
        /// </summary>
        /// <param name="envelope">The instance to serialize</param>
        /// <returns>The resulting XML string</returns>
        public string ToXmlString(SoapEnvelopeOld envelope)
        {
            if (envelope == null) return null;

            try
            {
                using (var textWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(textWriter, XmlWriterSettings))
                {
                    new XmlSerializer(typeof(SoapEnvelopeOld))
                        .Serialize(xmlWriter, envelope, XmlSerializerNamespaces);
                    return textWriter.ToString();
                }
            }
            catch (Exception e)
            {
                throw new SoapEnvelopeSerializationException(envelope, e);
            }
        }

        /// <summary>
        /// Deserializes a given XML string into a <see cref="SoapEnvelopeOld"/>.
        /// </summary>
        /// <param name="xml">The XML string do deserialize</param>
        /// <returns>The resulting <see cref="SoapEnvelopeOld"/></returns>
        public SoapEnvelopeOld ToSoapEnvelope(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return null;

            try
            {
                using (var textWriter = new StringReader(xml))
                {
                    var result = (SoapEnvelopeOld)new XmlSerializer(typeof(SoapEnvelopeOld)).Deserialize(textWriter);

                    return result;
                }
            }
            catch (Exception e)
            {
                throw new SoapEnvelopeDeserializationException(xml, e);
            }
        }

        #endregion
    }
}
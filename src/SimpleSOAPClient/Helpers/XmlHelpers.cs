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
namespace SimpleSOAPClient.Helpers
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Helper class with extensions for XML manipulation
    /// </summary>
    internal static class XmlHelpers
    {
        private static readonly XmlSerializerNamespaces EmptyXmlSerializerNamespaces;

        static XmlHelpers()
        {
            EmptyXmlSerializerNamespaces = new XmlSerializerNamespaces();
            EmptyXmlSerializerNamespaces.Add("xs", Constant.Namespace.OrgW3Www2001XmlSchema);
            EmptyXmlSerializerNamespaces.Add("xml", Constant.Namespace.OrgW3WwwXml1998Namespace);
            EmptyXmlSerializerNamespaces.Add("env", Constant.Namespace.OrgW3Www200305SoapEnvelope);
        }

        /// <summary>
        /// Serializes a given object to XML and returns the <see cref="XElement"/> representation.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="item">The item to convert</param>
        /// <returns>The object as a <see cref="XElement"/></returns>
        public static XElement ToXElement<T>(this T item)
        {
            if (item == null)
                return null;

            using (var textWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings
            {
                OmitXmlDeclaration = false,
                Indent = false,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            }))
            {
                new XmlSerializer(item.GetType())
                    .Serialize(xmlWriter, item, EmptyXmlSerializerNamespaces);
                return XElement.Parse(textWriter.ToString());
            }
        }

        /// <summary>
        /// Deserializes a given <see cref="XElement"/> to a new object of the expected type.
        /// If null the default(T) will be returned.
        /// </summary>
        /// <typeparam name="T">The type to be deserializable</typeparam>
        /// <param name="xml">The <see cref="XElement"/> to deserialize</param>
        /// <returns>The deserialized object</returns>
        public static T ToObject<T>(this XElement xml)
        {
            if (xml == null)
                return default(T);

            using (var textWriter = new StringReader(xml.ToString()))
            {
                var result = (T)new XmlSerializer(typeof(T)).Deserialize(textWriter);

                return result;
            }
        }
    }
}
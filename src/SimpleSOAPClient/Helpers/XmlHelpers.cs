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

using System.Runtime.CompilerServices;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System;

[assembly: InternalsVisibleTo("SimpleSOAPClient.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100e9d4b3865537a3" +
                                                                "5aabe1076ab836c58e47a3315970568d17b1d58b6d08a648e6333a714112adeb9481d79cd3a529" +
                                                                "ab6ee0e643b9098fa703ca085202968cb4792fc1ccc0d85fff62ed01993ed67f5cf5c2fac83622" +
                                                                "e019654eab372c6c4ecefbc8198b267ed757b30da82779857ca6861204961aa175ef48a7e79ad3" +
                                                                "b0d754bd")]
namespace SimpleSOAPClient.Helpers
{




    /// <summary>
    /// Helper class with extensions for XML manipulation
    /// </summary>
    internal static class XmlHelpers
    {
        private static readonly XmlSerializerNamespaces EmptyXmlSerializerNamespaces;

        static XmlHelpers()
        {
            EmptyXmlSerializerNamespaces = new XmlSerializerNamespaces();
            EmptyXmlSerializerNamespaces.Add("", "");
        }

        /// <summary>
        /// Serializes the given object to a XML string
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="item">The item to serialize</param>
        /// <param name="removeXmlDeclaration">Remove the XML declaration</param>
        /// <returns>The XML string</returns>
        public static string ToXmlString<T>(this T item, bool removeXmlDeclaration)
        {
            if (item == null) return null;

            using (var textWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings
            {
                OmitXmlDeclaration = removeXmlDeclaration,
                Indent = false,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            }))
            {
#if NETSTANDARD2_0 || NET45
                if (Attribute.IsDefined(item.GetType(), typeof(System.Runtime.Serialization.DataContractAttribute)))
                {
                    var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
                    serializer.WriteObject(xmlWriter, item);
                    xmlWriter.Flush();
                    return textWriter.ToString();
                }
#endif
                new XmlSerializer(item.GetType())
                    .Serialize(xmlWriter, item, EmptyXmlSerializerNamespaces);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Serializes the given object to a XML string
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="item">The item to serialize</param>
        /// <returns>The XML string</returns>
        public static string ToXmlString<T>(this T item)
        {
            return item.ToXmlString(true);
        }

        /// <summary>
        /// Serializes a given object to XML and returns the <see cref="XElement"/> representation.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="item">The item to convert</param>
        /// <param name="removeXmlDeclaration">Remove the XML declaration</param>
        /// <returns>The object as a <see cref="XElement"/></returns>
        public static XElement ToXElement<T>(this T item, bool removeXmlDeclaration)
        {
            return item == null ? null : XElement.Parse(item.ToXmlString(removeXmlDeclaration));
        }

        /// <summary>
        /// Serializes a given object to XML and returns the <see cref="XElement"/> representation.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="item">The item to convert</param>
        /// <returns>The object as a <see cref="XElement"/></returns>
        public static XElement ToXElement<T>(this T item)
        {
            return item.ToXElement(false);
        }

        /// <summary>
        /// Deserializes a given XML string to a new object of the expected type.
        /// If null or white spaces the default(T) will be returned;
        /// </summary>
        /// <typeparam name="T">The type to be deserializable</typeparam>
        /// <param name="xml">The XML string to deserialize</param>
        /// <returns>The deserialized object</returns>
        public static T ToObject<T>(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return default(T);

            using (var stringReader = new StringReader(xml))
            using (var xmlReader = XmlReader.Create(stringReader))
            {
#if NETSTANDARD2_0 || NET45
                if (Attribute.IsDefined(typeof(T), typeof(System.Runtime.Serialization.DataContractAttribute)))
                {
                    var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
                    return (T)serializer.ReadObject(xmlReader);
                }
#endif

                var result = (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
                return result;
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
            return xml == null ? default(T) : xml.ToString().ToObject<T>();
        }
    }
}
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
    using System.Xml.Linq;
    using Models;
    using Models.V1Dot1;

    /// <summary>
    /// Provider for serialization and deserialization of <see cref="SoapEnvelope"/> instances.
    /// </summary>
    public interface ISoapEnvelopeSerializationProvider
    {
        /// <summary>
        /// Serializes a given <see cref="SoapEnvelope"/> instance into a XML string.
        /// </summary>
        /// <param name="envelope">The instance to serialize</param>
        /// <returns>The resulting XML string</returns>
        string ToXmlString(SoapEnvelope envelope);

        /// <summary>
        /// Serializes a given <see cref="Models.V1Dot2.SoapEnvelope"/> instance into a XML string.
        /// </summary>
        /// <param name="envelope">The instance to serialize</param>
        /// <returns>The resulting XML string</returns>
        string ToXmlString(Models.V1Dot2.SoapEnvelope envelope);

        /// <summary>
        /// Deserializes a given XML string into a <see cref="SoapEnvelope"/>.
        /// </summary>
        /// <param name="xml">The XML string do deserialize</param>
        /// <returns>The resulting <see cref="SoapEnvelope"/></returns>
        SoapEnvelope ToSoapEnvelopeV1Dot1(string xml);

        /// <summary>
        /// Deserializes a given XML string into a <see cref="Models.V1Dot2.SoapEnvelope"/>.
        /// </summary>
        /// <param name="xml">The XML string do deserialize</param>
        /// <returns>The resulting <see cref="Models.V1Dot2.SoapEnvelope"/></returns>
        Models.V1Dot2.SoapEnvelope ToSoapEnvelopeV1Dot2(string xml);

        /// <summary>
        /// Converts the given item into a <see cref="XElement"/>.
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="item">The item to be converted</param>
        /// <returns>The resulting XElement</returns>
        XElement ToXElement<T>(T item);

        /// <summary>
        /// Converts the given <see cref="XElement"/> into an item of expected type.
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="element">The element to be converted</param>
        /// <returns>The resulting item</returns>
        T ToObject<T>(XElement element);
    }
}

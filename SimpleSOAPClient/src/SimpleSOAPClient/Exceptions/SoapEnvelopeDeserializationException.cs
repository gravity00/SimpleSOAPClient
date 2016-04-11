#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 SimplePersistence
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
namespace SimpleSOAPClient.Exceptions
{
    using System;
    using Models;

    /// <summary>
    /// Exception thrown when an exception is thrown when deserializing
    /// a given XML string to a <see cref="SoapEnvelope"/>.
    /// </summary>
    public class SoapEnvelopeDeserializationException : SoapClientException
    {
        private const string DefaultErrorMessage = "Failed to deserialize the XML string to a SOAP Envelope";

        /// <summary>
        /// The XML string that was beeing deserialized
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public string XmlValue { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="SoapEnvelopeDeserializationException"/>
        /// </summary>
        /// <param name="xmlValue">The XML string that was beeing deserialized</param>
        /// <exception cref="ArgumentNullException"/>
        public SoapEnvelopeDeserializationException(string xmlValue) : this(xmlValue, DefaultErrorMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="SoapEnvelopeDeserializationException"/>
        /// </summary>
        /// <param name="xmlValue">The XML string that was beeing deserialized</param>
        /// <param name="message">The message to be used</param>
        /// <exception cref="ArgumentNullException"/>
        public SoapEnvelopeDeserializationException(string xmlValue, string message) : base(message)
        {
            if (xmlValue == null) throw new ArgumentNullException(nameof(xmlValue));

            XmlValue = xmlValue;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SoapEnvelopeDeserializationException"/>
        /// </summary>
        /// <param name="xmlValue">The XML string that was beeing deserialized</param>
        /// <param name="message">The message to be used</param>
        /// <param name="innerException">The inner exception</param>
        /// <exception cref="ArgumentNullException"/>
        public SoapEnvelopeDeserializationException(string xmlValue, string message, Exception innerException) : base(message, innerException)
        {
            if (xmlValue == null) throw new ArgumentNullException(nameof(xmlValue));

            XmlValue = xmlValue;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SoapEnvelopeDeserializationException"/>
        /// </summary>
        /// <param name="xmlValue">The XML string that was beeing deserialized</param>
        /// <param name="innerException">The inner exception</param>
        /// <exception cref="ArgumentNullException"/>
        public SoapEnvelopeDeserializationException(string xmlValue, Exception innerException) : this(xmlValue, DefaultErrorMessage, innerException)
        {

        }
    }
}
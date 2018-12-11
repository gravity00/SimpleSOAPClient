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

    /// <summary>
    /// Represents settings to be used by SOAP clients
    /// </summary>
    public sealed class SoapClientSettings
    {
        #region Statics

        private static SoapClientSettings _default;

        /// <summary>
        /// The default <see cref="SoapClientSettings"/> to be used.
        /// </summary>
        public static SoapClientSettings Default
        {
            get { return _default; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _default = value;
            }
        }

        static SoapClientSettings()
        {
            _default = new SoapClientSettings();
        }

        #endregion

        private ISoapEnvelopeSerializationProvider _serializationProvider;

        /// <summary>
        /// The SOAP Envelope serialization provider
        /// </summary>
        public ISoapEnvelopeSerializationProvider SerializationProvider
        {
            get { return _serializationProvider; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _serializationProvider = value;
            }
        }

        /// <summary>
        /// Creates a new instance with default values
        /// </summary>
        public SoapClientSettings()
        {
            _serializationProvider = new SoapEnvelopeSerializationProvider();
        }
    }
}

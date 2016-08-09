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
    using Models.V1Dot2;

    /// <summary>
    /// Helper class to prepare SOAP messages
    /// </summary>
    public static class SoapMessage
    {
        /// <summary>
        /// SOAP Version 1.1 messages
        /// </summary>
        public static class V1Dot1
        {
            /// <summary>
            /// Prepares a SOAP Version 1.1 Envelope
            /// </summary>
            /// <returns>The SOAP Envelope</returns>
            public static Models.SoapEnvelope PrepareSoapEnvelope()
            {
                return new Models.SoapEnvelope();
            }
        }

        /// <summary>
        /// SOAP Version 1.2 messages
        /// </summary>
        public static class V1Dot2
        {
            /// <summary>
            /// Prepares a SOAP Version 1.2 Envelope
            /// </summary>
            /// <returns>The SOAP Envelope</returns>
            public static SoapEnvelope PrepareSoapEnvelope()
            {
                return new SoapEnvelope();
            }
        }
    }
}

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
namespace SimpleSOAPClient.Exceptions
{
    using System;
    using System.Xml.Linq;
    using Models.V1_2;

    /// <summary>
    /// Exception representing a fault returned by the server
    /// </summary>
    public class FaultV1Dot2Exception : SoapClientException
    {
        private const string DefaultErrorMessage = "A SOAP 1.2 Fault was returned by the server";

        /// <summary>
        /// The fault code
        /// </summary>
        public SoapFaultCode Code { get; set; }

        /// <summary>
        /// The fault reason
        /// </summary>
        public SoapFaultReason Reason { get; set; }

        /// <summary>
        /// The fault node
        /// </summary>
        public string Node { get; set; }

        /// <summary>
        /// The fault role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// The fault detail
        /// </summary>
        public XElement Detail { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultV1Dot2Exception"/> with 
        /// a default error message
        /// </summary>
        public FaultV1Dot2Exception() : base(DefaultErrorMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultV1Dot2Exception"/> with 
        /// a specified error message
        /// </summary>
        /// <param name="message">The error message</param>
        public FaultV1Dot2Exception(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultV1Dot2Exception"/> with 
        /// a specified error message and a reference to the inner exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public FaultV1Dot2Exception(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultV1Dot2Exception"/> with 
        /// a specified error message and a reference to the inner exception
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public FaultV1Dot2Exception(Exception innerException) : base(DefaultErrorMessage, innerException)
        {

        }
    }
}

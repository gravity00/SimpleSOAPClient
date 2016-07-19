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

    /// <summary>
    /// Exception representing a fault returned by the server
    /// </summary>
    public class FaultException : SoapClientException
    {
        private const string DefaultErrorMessage = "A fault was returned by the server";

        /// <summary>
        /// The fault code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The fault string
        /// </summary>
        public string String { get; set; }

        /// <summary>
        /// The fault actor
        /// </summary>
        public string Actor { get; set; }

        /// <summary>
        /// The fault detail
        /// </summary>
        public XElement Detail { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultException"/> with 
        /// a default error message
        /// </summary>
        public FaultException() : base(DefaultErrorMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultException"/> with 
        /// a specified error message
        /// </summary>
        /// <param name="message">The error message</param>
        public FaultException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultException"/> with 
        /// a specified error message and a reference to the inner exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public FaultException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="FaultException"/> with 
        /// a specified error message and a reference to the inner exception
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public FaultException(Exception innerException) : base(DefaultErrorMessage, innerException)
        {

        }
    }
}

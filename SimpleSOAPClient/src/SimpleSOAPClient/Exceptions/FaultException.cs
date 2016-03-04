namespace SimpleSOAPClient.Exceptions
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// Exception representing a fault returned by the server
    /// </summary>
    public class FaultException : Exception
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

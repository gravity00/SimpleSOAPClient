namespace SimpleSOAPClient.Exceptions
{
    using System;
    using System.Xml.Linq;

    public class FaultException : Exception
    {
        private const string DefaultErrorMessage = "A fault was returned by the server";

        public string FaultCode { get; set; }

        public string FaultString { get; set; }

        public string FaultActor { get; set; }

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

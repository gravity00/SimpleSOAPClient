using System;

namespace SimpleSOAPClient
{
    /// <summary>
    /// Represents a SOAP body
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class SoapEnvelopeBody<TContent> where TContent : class
    {
        private TContent _content;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="content">The body content</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SoapEnvelopeBody(TContent content)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Encoding style
        /// </summary>
        public string EncodingStyle { get; set; }

        /// <summary>
        /// The body content
        /// </summary>
        public TContent Content
        {
            get => _content;
            set => _content = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
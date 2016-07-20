namespace SimpleSOAPClient.Handlers
{
    using System.Net.Http;
    using Models;

    /// <summary>
    /// Represents an handler for SOAP requests and responses.
    /// </summary>
    public interface ISoapHandler
    {
        /// <summary>
        /// The order for which the handler will be executed
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        void BeforeSoapEnvelopeSerialization(ISoapClient client, BeforeSoapEnvelopeSerializationArguments arguments);

        /// <summary>
        /// Method invoked before sending the <see cref="HttpRequestMessage"/> to the server.
        /// Useful to log the request or change properties like HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        void BeforeHttpRequest(ISoapClient client, BeforeHttpRequestArguments arguments);

        /// <summary>
        /// Method invoked after receiving a <see cref="HttpResponseMessage"/> from the server.
        /// Useful to log the response or validate HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        void AfterHttpResponse(ISoapClient client, AfterHttpResponseArguments arguments);

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        void AfterSoapEnvelopeDeserialization(ISoapClient client, AfterSoapEnvelopeDeserializationArguments arguments);
    }
}

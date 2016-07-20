namespace SimpleSOAPClient.Handlers
{
    using System;
    using System.Net.Http;
    using Models;

    /// <summary>
    /// SOAP Handler that exposes delegates for each handling operation.
    /// </summary>
    public sealed class DelegatingSoapHandler : ISoapHandler
    {
        /// <summary>
        /// Delegate for <see cref="ISoapHandler.BeforeSoapEnvelopeSerialization"/> method.
        /// </summary>
        public Action<ISoapClient, BeforeSoapEnvelopeSerializationArguments> BeforeSoapEnvelopeSerializationDelegate { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.BeforeHttpRequest"/> method.
        /// </summary>
        public Action<ISoapClient, BeforeHttpRequestArguments> BeforeHttpRequestDelegate { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.AfterHttpResponse"/> method.
        /// </summary>
        public Action<ISoapClient, AfterHttpResponseArguments> AfterHttpResponseDelegate { get; set; }

        /// <summary>
        /// Delegate for <see cref="ISoapHandler.AfterSoapEnvelopeDeserialization"/> method.
        /// </summary>
        public Action<ISoapClient, AfterSoapEnvelopeDeserializationArguments> AfterSoapEnvelopeDeserializationDelegate { get; set; }

        #region Implementation of ISoapHandler

        /// <summary>
        /// The order for which the handler will be executed
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Method invoked before serializing a <see cref="SoapEnvelope"/>. 
        /// Useful to add properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void BeforeSoapEnvelopeSerialization(ISoapClient client, BeforeSoapEnvelopeSerializationArguments arguments)
        {
            BeforeSoapEnvelopeSerializationDelegate?.Invoke(client, arguments);
        }

        /// <summary>
        /// Method invoked before sending the <see cref="HttpRequestMessage"/> to the server.
        /// Useful to log the request or change properties like HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void BeforeHttpRequest(ISoapClient client, BeforeHttpRequestArguments arguments)
        {
            BeforeHttpRequestDelegate?.Invoke(client, arguments);
        }

        /// <summary>
        /// Method invoked after receiving a <see cref="HttpResponseMessage"/> from the server.
        /// Useful to log the response or validate HTTP headers.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void AfterHttpResponse(ISoapClient client, AfterHttpResponseArguments arguments)
        {
            AfterHttpResponseDelegate?.Invoke(client, arguments);
        }

        /// <summary>
        /// Method invoked after deserializing a <see cref="SoapEnvelope"/> from the server response. 
        /// Useful to validate properties like <see cref="SoapHeader"/>.
        /// </summary>
        /// <param name="client">The client sending the request</param>
        /// <param name="arguments">The method arguments</param>
        public void AfterSoapEnvelopeDeserialization(ISoapClient client, AfterSoapEnvelopeDeserializationArguments arguments)
        {
            AfterSoapEnvelopeDeserializationDelegate?.Invoke(client, arguments);
        }

        #endregion
    }
}
namespace SimpleSOAPClient
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface ISoapClient
    {
        /// <summary>
        /// Handler that can manipulate the <see cref="SoapEnvelope"/>
        /// before serialization.
        /// </summary>
        Func<string, SoapEnvelope, SoapEnvelope> RequestEnvelopeHandler { get; set; }

        /// <summary>
        /// Handler that can manipulate the generated XML string.
        /// </summary>
        Func<string, string, string> RequestRawHandler { get; set; }

        /// <summary>
        /// Handler that can manipulate the <see cref="SoapEnvelope"/> returned
        /// by the SOAP Endpoint.
        /// </summary>
        Func<string, SoapEnvelope, SoapEnvelope> ResponseEnvelopeHandler { get; set; }

        /// <summary>
        /// Handler that can manipulate the returned string before deserialization.
        /// </summary>
        Func<string, string, string> ResponseRawHandler { get; set; }

        /// <summary>
        /// Sends the given <see cref="SoapEnvelope"/> into the specified url.
        /// </summary>
        /// <param name="url">The url that will receive the request</param>
        /// <param name="requestEnvelope">The <see cref="SoapEnvelope"/> to be sent</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited for the result</returns>
        Task<SoapEnvelope> SendAsync(
            string url, SoapEnvelope requestEnvelope, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Sends the given <see cref="SoapEnvelope"/> into the specified url.
        /// </summary>
        /// <param name="url">The url that will receive the request</param>
        /// <param name="requestEnvelope">The <see cref="SoapEnvelope"/> to be sent</param>
        /// <returns>The resulting <see cref="SoapEnvelope"/></returns>
        SoapEnvelope Send(string url, SoapEnvelope requestEnvelope);
    }
}
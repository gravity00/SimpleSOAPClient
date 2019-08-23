using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSOAPClient
{
    /// <summary>
    /// The SOAP client that can be used to invoke SOAP Endpoints
    /// </summary>
    public interface ISoapClient
    {
        /// <summary>
        /// The service endpoint address
        /// </summary>
        Uri EndpointAddress { get; }

        /// <summary>
        /// Sends the <see cref="SoapRequestMessage"/> into the configured SOAP endpoint.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<ISoapResponseMessage> SendAsync(SoapRequestMessage request, CancellationToken ct);
    }
}

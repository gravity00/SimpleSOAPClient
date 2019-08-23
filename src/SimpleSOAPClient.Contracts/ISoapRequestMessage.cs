using System.Threading;
using System.Threading.Tasks;

namespace SimpleSOAPClient
{
    /// <summary>
    /// The SOAP request message
    /// </summary>
    public interface ISoapRequestMessage
    {
        /// <summary>
        /// Sends the SOAP request
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns></returns>
        Task<ISoapResponseMessage> SendAsync(CancellationToken ct);
    }
}
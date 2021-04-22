using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleSOAPClient.Models;
using Xunit;

namespace SimpleSOAPClient.Tests
{
    public class MultipartResponseTests
    {
        private const string AttachmentContents = "The Attachment Contents";
        private const string AttachmentContentId = "test-content@test.com";

        [Fact]
        public async Task TestReceiveAndDeserializeSingleAttachment()
        {
            var httpMessageHandler = new TestHttpMessageHandler();
            var httpClient = new HttpClient(httpMessageHandler);
            var soapClient = new SoapClient(httpClient);

            var result = await soapClient.SendAsync("http://test.com", "", SoapEnvelope.Prepare());
            Assert.Equal(AttachmentContents, await result.Attachments[AttachmentContentId].ReadAsStringAsync());
        }
        
        
        private class TestHttpMessageHandler : HttpMessageHandler
        {

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var emptyEnvelope = new SoapEnvelopeSerializationProvider().ToXmlString(SoapEnvelope.Prepare());
                var multipartResponse = new MultipartContent("related")
                {
                    new StringContent(emptyEnvelope, Encoding.UTF8, "application/xop+xml"),
                    new StringContent(AttachmentContents, Encoding.UTF8, "text/plain")
                    {
                        Headers = {{"Content-Id", $"<{AttachmentContentId}>"}}
                    }
                };

                return Task.FromResult(new HttpResponseMessage
                {
                    Version = HttpVersion.Version11,
                    Content = multipartResponse,
                    StatusCode = HttpStatusCode.OK,
                    ReasonPhrase = "OK",
                });
            }
        }
    }
}
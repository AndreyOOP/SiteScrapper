using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ParserApi.Tests.Mocks
{
    internal class HttpMessageHandlerMock : HttpMessageHandler
    {
        private Dictionary<string, HttpResponseMessage> responses;

        public HttpMessageHandlerMock(Dictionary<string, HttpResponseMessage> responses) // or Uri - ?
        {
            this.responses = responses;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri.ToString();
            
            if (!responses.ContainsKey(uri))
                throw new KeyNotFoundException("No response setup for provided Uri");

            return Task.FromResult(responses[uri]);
        }
    }
}

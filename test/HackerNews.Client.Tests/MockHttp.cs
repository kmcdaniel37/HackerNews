using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Client.Tests
{
    // ReSharper disable once UnusedMember.Global
    public static  class MockHttp
    {    
        
        public static HttpClient Create(
            Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsyncOverride)
        {
            return new HttpClient(new FakeResponseHandler(sendAsyncOverride));
        }
        private class FakeResponseHandler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _sendAsyncOverride;

            public FakeResponseHandler(
                Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsyncOverride)
            {
                _sendAsyncOverride = sendAsyncOverride;
            }

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                return _sendAsyncOverride(request);
            }
        }
    }
}
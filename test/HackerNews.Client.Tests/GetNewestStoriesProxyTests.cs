using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HackerNews.Common.Dto;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace HackerNews.Client.Tests
{
    public class GetNewestStoriesProxyTests
    {
        private const string BaseUri = "http://localhost";
        private readonly string _expectedUri = $"{BaseUri}/api/Stories";
        private readonly GetNewestStoriesProxy _proxy;
        private static readonly StoryDto CreateStoryDto = CreateDto();

        public GetNewestStoriesProxyTests()
        {
            _proxy = new GetNewestStoriesProxy(BaseUri);
        }

        [Fact]
        public async Task GivenRequest_GetAsync_SendsRequest()
        {
            // Arrange
//            HttpRequestMessage request = null;
//            _proxy = () => MockHttp.Create(message =>
//            {
//                request = message;
//                var response = CreateSuccessResponse();
//                return Task.FromResult(response);
//            });

            //var messageHandler = new HttpMessageHandler();
            
            
            //// Act
            //await _proxy.GetAsync();

            //// Assert
            //request.Should().NotBeNull();
            //request.Method.Should().Be(HttpMethod.Get);
            //request.RequestUri.Should().Be(_expectedUri);
        }
//
//        [Fact]
//        public async Task GivenSuccessResponse_GetAsync_ReturnsStoryDto()
//        {
//            // Arrange
//            _proxy.CreateHttpClient = () => MockHttp.Create(message =>
//            {
//                var response = CreateSuccessResponse();
//                return Task.FromResult(response);
//            });
//
//            // Act
//            var result = await _proxy.GetAsync();
//
//            // Assert
//            result.Should().Be(Dto.Text);
//        }
//        
        private static StoryDto CreateDto()
        {
            return new StoryDto
            {
                Id = 4561,
                Author = "test Author",
                Title = "test Title"
            };
        }

        private static HttpResponseMessage CreateSuccessResponse()
        {
            var json = JsonConvert.SerializeObject(CreateStoryDto);
            var retval = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            return retval;
        }
    }
    }
}
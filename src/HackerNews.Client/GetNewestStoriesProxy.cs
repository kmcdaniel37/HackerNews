using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.Common.Dto;
using Newtonsoft.Json;

namespace HackerNews.Client
{
    public class GetNewestStoriesProxy : IGetNewestStoriesProxy
    {
        private readonly string _baseUri;

        public GetNewestStoriesProxy(string baseUri)
        {
            _baseUri = $"{baseUri}/1.0/api/Stories";
        }

        public async Task<List<StoryDto>> GetAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUri);
            var stories = await ReadAsJsonAsync<List<StoryDto>>(response);
            return stories;
        }

        private static async Task<T> ReadAsJsonAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jsonContent = JsonConvert.DeserializeObject<T>(content);
            return jsonContent;
        }
    }
}
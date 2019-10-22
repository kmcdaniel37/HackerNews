using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HackerNews.Data
{
    public class JsonContext
    {
        public static async Task<T> GetAsync<T>(string endPoint)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(endPoint);
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
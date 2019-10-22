using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Data.Models;

namespace HackerNews.Data.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        public async Task<IEnumerable<Items>> GetAsync()
        {
            var itemIds = await JsonContext.GetAsync<int[]>("https://hacker-news.firebaseio.com/v0/newstories.json");

            var items = await GetListOfItems(itemIds);
            return items.ToArray();
        }
        
        private static async Task<List<Items>> GetListOfItems(IEnumerable<int> itemIds)
        {
            var items = new List<Items>();
            foreach (var itemId in itemIds)
            {
                items.Add(await JsonContext.GetAsync<Items>($"https://hacker-news.firebaseio.com/v0/item/{itemId}.json"));
            }

            return items;
        }
    }
}
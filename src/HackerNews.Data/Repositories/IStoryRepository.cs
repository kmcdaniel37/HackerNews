using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Data.Models;

namespace HackerNews.Data.Repositories
{
    public interface IStoryRepository
    {
        Task<IEnumerable<Items>> GetAsync();
    }
}
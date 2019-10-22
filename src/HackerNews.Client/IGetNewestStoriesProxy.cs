using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Common.Dto;

namespace HackerNews.Client
{
    public interface IGetNewestStoriesProxy
    {
        Task<List<StoryDto>> GetAsync();
    }
}
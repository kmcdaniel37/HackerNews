using AutoMapper;
using HackerNews.Common.Dto;
using HackerNews.Data.Models;

namespace HackerNews.Api.Stories.Mapper
{
    // ReSharper disable once UnusedMember.Global
    public class StoriesProfile: Profile
    {
        public StoriesProfile()
        {
            CreateMap<Items, StoryDto>();
        }
    }
}
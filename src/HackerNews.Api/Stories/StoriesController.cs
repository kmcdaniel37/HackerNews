using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Common.Dto;
using HackerNews.Data.Models;
using HackerNews.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Api.Stories
{
    [Route("api/[controller]")]
    public class StoriesController : Controller
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memory;

        public StoriesController(IStoryRepository storyRepository, IMapper mapper, IMemoryCache memory)
        {
            _storyRepository = storyRepository;
            _mapper = mapper;
            _memory = memory;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            const string cacheKey = "NewestStories";
            IEnumerable<Items> stories;

            if (_memory.TryGetValue(cacheKey, out IEnumerable<Items> currentStories))
            {
                stories = currentStories;
            }
            else
            {
                stories = await _storyRepository.GetAsync();
                // ReSharper disable once PossibleMultipleEnumeration
                _memory.Set(cacheKey, stories, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)));
            }

            // ReSharper disable once PossibleMultipleEnumeration
            if (!stories.Any())
                return NoContent();
            
            // ReSharper disable once PossibleMultipleEnumeration
            var storyDto = _mapper.Map<List<Items>, List<StoryDto>>(stories.ToList());
            
            return Ok(storyDto);
        }
    }
}
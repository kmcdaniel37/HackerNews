using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HackerNews.Api.Stories;
using HackerNews.Common.Dto;
using HackerNews.Data.Models;
using HackerNews.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HackerNews.Api.Tests.Stories
{
    public class StoriesControllerTests
    {
        private readonly StoriesController _controller;
        private readonly Mock<IStoryRepository> _stubStoryRepository;
        private readonly Mock<IMapper> _stubMapper;
        
        private static readonly IEnumerable<Items> Items = new[]
        {
            new Items {Id = 123, Author = "test Author1", Title = "test title1"},
            new Items {Id = 234, Author = "test Author2", Title = "test title2"}
        };
        private static readonly List<StoryDto> StoryDtos = new List<StoryDto>
        {
            new StoryDto {Id = 123, Author = "test Author1", Title = "test title1"},
            new StoryDto {Id = 234, Author = "test Author2", Title = "test title2"}
        };
        
        public StoriesControllerTests()
        {
            _stubStoryRepository = new Mock<IStoryRepository>();
            _stubMapper = new Mock<IMapper>();
            _controller = new StoriesController(_stubStoryRepository.Object, _stubMapper.Object);
        }

        [Fact]
        public async Task GivenResult_GetAsync_ReturnsOk()
        {
            // Arrange
            SetIMapper();
            _stubStoryRepository.Setup(x => x.GetAsync()).ReturnsAsync(Items);

            // Act 
            var result = await _controller.GetAsync();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GivenNoResult_GetAsync_ReturnsNoContent()
        {
            // Arrange
            SetIMapper();
            _stubStoryRepository.Setup(x => x.GetAsync()).ReturnsAsync(new List<Items>());

            // Act 
            var result = await _controller.GetAsync();

            // Assert
            result.Should().NotBeNull().And.BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task HasData_GetAsync_ReturnsStories()
        {
            // Arrange
            SetIMapper();
            _stubStoryRepository.Setup(x => x.GetAsync()).ReturnsAsync(Items);

            // Act 
            var result = await _controller.GetAsync();

            // Assert
            ((ObjectResult)result).Value.Should().NotBeNull().And.BeOfType<List<StoryDto>>();
            var response = (IEnumerable<StoryDto>)((ObjectResult)result).Value;
            var dto = response.First();
            dto.Id.Should().Be(Items.First().Id);
            dto.Title.Should().Be(Items.First().Title);
            dto.Author.Should().Be(Items.First().Author);
        }
        
        private void SetIMapper()
        {
            _stubMapper.Setup(m => m.Map<List<Items>, List<StoryDto>>(It.IsAny<List<Items>>())).Returns(StoryDtos);
        }
    }
}
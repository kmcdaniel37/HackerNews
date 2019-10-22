using System.Linq;
using System.Threading.Tasks;
using HackerNews.Data.Repositories;
using Xunit;
using FluentAssertions;

namespace HackerNews.Data.Tests.Repositories
{
    public class StoryRepositoryTests
    {
        private readonly IStoryRepository _repository;
        
        public StoryRepositoryTests()
        {
            var context = new JsonContext();
            _repository = new StoryRepository(context);
        }

        [Fact]
        public async Task GetAsync_ReturnsStory()
        {
            // Arrange
            
            // Act
            var result = await _repository.GetAsync();
            
            // Assert
            result.Count().Should().BeGreaterThan(0);
        }
    }
}
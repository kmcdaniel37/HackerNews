using System.Threading.Tasks;
using Xunit;

namespace HackerNews.Data.Tests
{
    public class JsonContextTests
    {
        private const string BaseUri = "http://localhost";
        private readonly JsonContext _jsonContext;
        
        public JsonContextTests()
        {
            _jsonContext = new JsonContext();
        }

        [Fact]
        public async Task GivenValidEndPoint_GetAsync_ReturnsData()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
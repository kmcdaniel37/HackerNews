using System.Threading.Tasks;
using HackerNews.Client;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Web.Controllers
{
    [Route("HackerNews/[controller]")]
    public class StoriesController : Controller
    {
        private readonly IGetNewestStoriesProxy _getNewestStoriesProxy;

        public StoriesController(IGetNewestStoriesProxy getNewestStoriesProxy)
        {
            _getNewestStoriesProxy = getNewestStoriesProxy;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStories()
        {
            var result = await _getNewestStoriesProxy.GetAsync();
            return Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities;
using Infrastructure.Data;
using Logic.Managers;
using Logic.Entities;
using SearchOnGithub.RestApi.Entities;
using Infrastructure.Services;

namespace SearchOnGithubRestApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class FindController : ControllerBase
	{
		private readonly ILogger<FindController> _logger;
        private readonly SearchOnGithubContext _context;

        public FindController(ILogger<FindController> logger, SearchOnGithubContext context)
		{
			_logger = logger;
            _context = context;
        }

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] SearchQueryRequest request)
		{
			try
			{
				var result = await GithubManager.GetSearchCardsAsync(_context, request.SearchString);

                return Ok(JsonSerializer.Serialize(result));
            }
			catch (Exception exc)
			{
				return BadRequest();
            }
        }

		[HttpGet]
		public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
		{
			var pageList = GithubManager.GetPageList(_context, page, pageSize);

            return Ok(pageList);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			GithubManager.Delete(_context, id);

            return Ok($"Deleted {id}");
		}
	}
}

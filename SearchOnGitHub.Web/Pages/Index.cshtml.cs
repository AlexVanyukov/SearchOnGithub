using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Logic.Managers;
using Infrastructure.Data;


namespace SearchOnGitHub.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; 
        private readonly SearchOnGithubContext _context;

        public IndexModel(ILogger<IndexModel> logger, SearchOnGithubContext context)
        {
            _logger = logger;
            _context = context;
        }

        public string Cards { get; private set; }
        
        public async Task<IActionResult> OnPost(string searchText)
        {
            try
            {
                var result = await GithubManager.GetSearchCardsAsync(_context, searchText);

                return RedirectToPage("SearchQueries", new {result});
            }
            catch (Exception exc)
            {
                return BadRequest();
            }
        }


    }
}

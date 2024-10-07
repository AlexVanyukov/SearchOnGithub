using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Linq;
using Logic.Managers;
using Infrastructure.Data;

namespace SearchOnGitHub.Web.Pages
{
    public class SearchQueriesModel : PageModel
    {
        private readonly ILogger<SearchQueriesModel> _logger;
        private readonly SearchOnGithubContext _context;

        public SearchQueriesModel(ILogger<SearchQueriesModel> logger, SearchOnGithubContext context)
        {
            _logger = logger;
            _context = context;
        }

        public string Cards { get; private set; }

        public async void Set(string searchText)
        {
            try
            {
                var result = await GithubManager.GetSearchCardsAsync(_context, searchText);
                var qwe = JsonSerializer.Serialize(result);
                return;
            }
            catch (Exception exc)
            {
                return;
            }
        }
    }
}

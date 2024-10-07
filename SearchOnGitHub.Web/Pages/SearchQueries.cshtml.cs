using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace SearchOnGitHub.Web.Pages
{
    public class SearchQueriesModel : PageModel
    {
        private readonly ILogger<SearchQueriesModel> _logger;

        public SearchQueriesModel(ILogger<SearchQueriesModel> logger)
        {
            _logger = logger;
        }

        public string Message { get; private set; } = "";

        public void OnGet(string name)
        {
            Message = $"Name: {name}";
        }
    }
}

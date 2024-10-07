using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SearchOnGitHub.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string Message { get; private set; } = "";
        public void OnGet()
        {
            Message = "Введите свое имя";
        }

        public void OnPost(string username)
        {
            Message = $"Ваше имя: {username}";
        }
    }
}

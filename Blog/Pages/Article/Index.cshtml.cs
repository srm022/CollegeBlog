using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Article
{
    [Authorize]
    public class ArticleModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}

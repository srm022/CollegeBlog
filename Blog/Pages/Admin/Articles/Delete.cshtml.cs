using System.Threading.Tasks;
using Blog.Services.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Admin.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly IArticleService _articleService;

        public DeleteModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public string Title { get; set; }

        public void OnGet()
        {
            Title = _articleService.GetArticleTitleById(Id);
        }
        
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _articleService.DeleteArticle(Id);

            return RedirectToPage("/Admin/Articles/Index");
        }
    }
}
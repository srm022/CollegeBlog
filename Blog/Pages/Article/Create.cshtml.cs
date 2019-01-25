using System;
using System.Threading.Tasks;
using Blog.Services.Article;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Article
{
    [Authorize]
    public class CreateArticleModel : PageModel
    {
        [BindProperty]
        public Models.Article.CreateArticleModel Model { get; set; }

        private readonly IArticleService _articleService;

        public CreateArticleModel(IArticleService articleService)
        {
            _articleService = articleService;
        }
        
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(User.Identity.GetUserId());
                Model.UserId = userId;

                await CreateArticle();
                return RedirectToPage("/Index");
            }

            return Page();
        }

        private async Task CreateArticle()
        {
            await _articleService.CreateArticle(Model);
        }
    }
}

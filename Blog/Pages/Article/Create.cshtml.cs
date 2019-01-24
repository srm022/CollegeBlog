using System;
using System.Threading.Tasks;
using Blog.Services.Article;
using Blog.Services.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Article
{
    [Authorize]
    public class CreateArticleModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public CreateArticleModel(IArticleService articleService, 
            IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.CreateArticleModel Model { get; set; }

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

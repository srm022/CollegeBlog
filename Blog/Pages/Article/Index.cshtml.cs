using System;
using System.Threading.Tasks;
using Blog.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Pages.Article
{
    [Authorize]
    public class ArticleModel : PageModel
    {
        private readonly IUserService _service;

        public ArticleModel(IUserService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.CreateArticleModel Model { get; set; }

        //[Authorize]
        //public async Task<IActionResult> OnPost()
        //{
            
        //}
    }
}

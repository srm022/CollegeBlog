using System.Collections.Generic;
using AutoMapper;
using Blog.Models.Article;
using Blog.Services.Article;
using Blog.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        public List<DisplayArticleModel> Articles { get; set; }

        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public IndexModel(IArticleService articleService,
            IMapper mapper, IUserService userService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            var displayArticlesList = new List<DisplayArticleModel>();

            var articles = _articleService.GetAllArticles();

            Articles = _mapper.Map<List<DisplayArticleModel>>(articles);

            for (int i = 0; i < articles.Count; i++)
                Articles[i].Author = _userService.GetUsernameById(articles[i].UserId);

            return Page();
        }
    }
}
using System.Collections.Generic;
using Blog.Models.Article;
using Blog.Services.Article;
using Blog.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        public DisplayManyArticlesModel Model { get; set; }

        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public IndexModel(IArticleService articleService, IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;

            Model = new DisplayManyArticlesModel {Articles = new List<DisplayArticleModel>()};
        }

        public IActionResult OnGet()
        {
            var articles = _articleService.GetAllArticles();

            foreach (var article in articles)
            {
                var author = _userService.GetUsernameById(article.UserId);
                Model.Articles.Add(new DisplayArticleModel
                {
                    DatePublished = article.DatePublished,
                    Author = author,
                    Title = article.Title,
                    ArticleId = article.ArticleId,
                    ArticleCategoryId = article.ArticleCategoryId,
                    Content = article.Content,
                    Comments = article.Comments
                });
            }

            return Page();
        }
    }
}
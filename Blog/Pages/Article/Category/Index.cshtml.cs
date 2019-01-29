using System.Collections.Generic;
using Blog.Models.Article;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Article.Category
{
    public class IndexModel : PageModel
    {
        public DisplayManyArticlesModel Model { get; set; }

        [BindProperty(SupportsGet = true)]
        public ArticleCategory CategoryId { get; set; }

        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public IndexModel(IArticleService articleService,
            IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        public void OnGet()
        {
            Model = new DisplayManyArticlesModel{Articles = new List<DisplayArticleModel>()};

            GetPublishedArticlesForCategory();
            Page();
        }

        private void GetPublishedArticlesForCategory()
        {
            var articles = _articleService.GetAllArticlesForCategory(CategoryId);

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
                    Content = article.Content
                });
            }
        }
    }
}
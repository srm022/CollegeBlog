using System.Collections.Generic;
using AutoMapper;
using Blog.Entities;
using Blog.Models.PageContent.Article;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Admin.Articles
{
    public class ArticlesModel : PageModel
    {
        [BindProperty]
        public DisplayManyArticlesModel Model { get; set; }

        private readonly IUserService _userService;
        private readonly IArticleService _articleService;
        
        public ArticlesModel(IUserService userService, 
            IArticleService articleService)
        {
            _userService = userService;
            _articleService = articleService;

            Model = new DisplayManyArticlesModel {Articles = new List<DisplayArticleModel>()};
        }

        public void OnGet()
        {
            GetPublishedArticles();
            Page();
        }

        private void GetPublishedArticles()
        {
            var articles = GetAllArticles();

            AttachAuthorsToArticles(articles);
        }

        private void AttachAuthorsToArticles(List<ArticleEntity> articles)
        {
            foreach (var article in articles)
            {
                var author = _userService.GetUsernameById(article.UserId);
                Model.Articles.Add(new DisplayArticleModel
                {
                    DatePublished = article.DatePublished,
                    Author = author,
                    Title = article.Title,
                    ArticleId = article.ArticleId
                });
            }
        }

        private List<ArticleEntity> GetAllArticles()
        {
            var articles = _articleService.GetAllArticles();

            return articles;
        }
    }
}
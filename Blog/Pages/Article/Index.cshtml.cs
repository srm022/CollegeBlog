using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Entities;
using Blog.Models.Article.Comment;
using Blog.Models.PageContent.Article;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Article
{
    public class ArticleModel : PageModel
    {
        public DisplayArticleModel Model { get; set; }

        [BindProperty]
        public CreateCommentModel CreateModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public ArticleModel(IArticleService articleService,
            IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;

            Model = new DisplayArticleModel {Comments = new List<CommentEntity>()};
            CreateModel = new CreateCommentModel();
        }

        public void OnGet()
        {
            GetArticle();
            GetCommentsForArticle();

            Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
                await CreateCommentOnArticle();

            return RedirectToPage();
        }

        private async Task CreateCommentOnArticle()
        {
            CreateModel.ArticleId = Id;
            await _articleService.CreateComment(CreateModel);
        }

        private void GetArticle()
        {
            var article = _articleService.GetArticleById(Id);

            var author = _userService.GetUsernameById(article.UserId);

            Model = new DisplayArticleModel
            {
                DatePublished = article.DatePublished,
                Author = author,
                Content = article.Content,
                Title = article.Title,
                ArticleCategoryId = article.ArticleCategoryId
            };
        }

        private void GetCommentsForArticle()
        {
            Model.Comments = _articleService.GetCommentsForArticleId(Id);
        }
    }
}

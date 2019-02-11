using System.Threading.Tasks;
using Blog.Entities;
using Blog.Models.PageContent.Article;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Admin.Articles
{
    public class UpdateModel : PageModel
    {
        private readonly IArticleService _articleService;

        public UpdateModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty]
        public UpdateArticleModel UpdateArticleModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public void OnGet()
        {
            PrepareArticleEdition();
        }

        private void PrepareArticleEdition()
        {
            var article = _articleService.GetArticleById(Id);

            UpdateArticleModel = new UpdateArticleModel
            {
                Title = article.Title,
                Content = article.Content,
                ArticleCategoryId = article.ArticleCategoryId
            };
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            UpdateArticleModel.ArticleId = Id;
            await _articleService.UpdateArticle(UpdateArticleModel);

            return RedirectToPage("/Admin/Articles/Index");
        }
    }
}
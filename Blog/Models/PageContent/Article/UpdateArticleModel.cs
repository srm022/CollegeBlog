using System.ComponentModel.DataAnnotations;
using Blog.Models.Article;

namespace Blog.Models.PageContent.Article
{
    public class UpdateArticleModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public ArticleCategory ArticleCategoryId { get; set; }

        public int ArticleId { get; set; }
    }
}
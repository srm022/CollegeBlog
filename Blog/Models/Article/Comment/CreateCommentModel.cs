using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Article.Comment
{
    public class CreateCommentModel
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        [Required]
        public string Content { get; set; }

        public int ArticleId { get; set; }
    }
}
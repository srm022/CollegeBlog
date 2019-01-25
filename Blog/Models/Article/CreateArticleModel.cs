using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Article
{
    public class CreateArticleModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public DateTime DatePublished { get; set; }

        public int UserId { get; set; }
    }
}
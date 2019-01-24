using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities
{
    public class ArticleEntity
    {
        [Key]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public DateTime DatePublished { get; set; }
        public string Content { get; set; }
    }
}
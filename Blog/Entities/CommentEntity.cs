using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities
{
    public class CommentEntity
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Blog.Models.Article.Comment;

namespace Blog.Models.Article
{
    public class DisplayArticleModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }
        public ArticleCategory ArticleCategoryId { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}
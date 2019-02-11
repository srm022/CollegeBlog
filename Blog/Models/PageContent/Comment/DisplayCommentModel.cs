using System;

namespace Blog.Models.PageContent.Comment
{
    public class DisplayCommentModel
    {
        public int CommentId { get; set; }
        public string ArticleTitle { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
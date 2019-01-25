﻿using System;

namespace Blog.Models.Article
{
    public class DisplayArticleModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }
    }
}
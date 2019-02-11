using System.Collections.Generic;
using Blog.Entities;
using Blog.Models.PageContent.Comment;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Admin.Comments
{
    public class CommentsModel : PageModel
    {
        [BindProperty]
        public DisplayManyCommentsModel Model { get; set; }

        private readonly IArticleService _articleService;

        public CommentsModel(IArticleService articleService)
        {
            _articleService = articleService;

            Model = new DisplayManyCommentsModel { Comments = new List<DisplayCommentModel>()};
        }

        public void OnGet()
        {
            GetPublishedComments();
            Page();
        }

        private void GetPublishedComments()
        {
            var comments = GetAllComments();

            AttachArticlesTitlesToModels(comments);
        }

        private void AttachArticlesTitlesToModels(List<CommentEntity> comments)
        {
            foreach (var comment in comments)
            {
                var title = _articleService.GetArticleTitleById(comment.ArticleId);
                Model.Comments.Add(new DisplayCommentModel
                {
                    DatePublished = comment.DatePublished,
                    Author = comment.Author,
                    ArticleTitle = title,
                    Content = comment.Content,
                    CommentId = comment.CommentId
                });
            }
        }

        private List<CommentEntity> GetAllComments()
        {
            var comments = _articleService.GetAllComments();

            return comments;
        }
    }
}
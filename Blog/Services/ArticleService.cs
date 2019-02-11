using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entities;
using Blog.Infrastructure.Database;
using Blog.Models.Article;
using Blog.Models.Article.Comment;
using Blog.Models.PageContent.Article;

namespace Blog.Services
{
    public interface IArticleService
    {
        List<ArticleEntity> GetAllArticles();
        List<ArticleEntity> GetAllArticlesForCategory(ArticleCategory category);
        Task CreateArticle(CreateArticleModel model);
        ArticleEntity GetArticleById(int articleId);
        string GetArticleTitleById(int id);
        Task<int> DeleteArticle(int id);
        Task UpdateArticle(UpdateArticleModel updateModel);

        Task CreateComment(CreateCommentModel model);
        List<CommentEntity> GetAllComments();
        CommentEntity GetCommentById(int commentId);
        List<CommentEntity> GetCommentsForArticleId(int articleId);
        Task<int> DeleteComment(int commentId);
    }

    public class ArticleService : IArticleService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public ArticleService(DataContext db, 
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<ArticleEntity> GetAllArticles()
        {
            var articles = _db.Article.OrderByDescending(d => d.DatePublished).ToList();

            return articles;
        }

        public List<ArticleEntity> GetAllArticlesForCategory(ArticleCategory category)
        {
            var articles = _db.Article.Where(a => a.ArticleCategoryId == category).OrderByDescending(d => d.DatePublished).ToList();

            return articles;
        }

        public ArticleEntity GetArticleById(int articleId)
        {
            return _db.Article.SingleOrDefault(a => a.ArticleId == articleId);
        }

        public CommentEntity GetCommentById(int commentId)
        {
            return _db.Comment.SingleOrDefault(c => c.CommentId == commentId);
        }

        public List<CommentEntity> GetCommentsForArticleId(int articleId)
        {
            return _db.Comment.Where(c => c.ArticleId == articleId).ToList();
        }

        public async Task<int> DeleteComment(int commentId)
        {
            var comment = _db.Comment.SingleOrDefault(u => u.CommentId == commentId);

            _db.Comment.Remove(comment ?? throw new InvalidOperationException());
            return await _db.SaveChangesAsync();
        }

        public string GetArticleTitleById(int id)
        {
            var user = _db.Article.SingleOrDefault(u => u.ArticleId == id);

            if (user == null)
                return "Title";

            return user.Title;
        }

        public async Task<int> DeleteArticle(int id)
        {
            var article = _db.Article.SingleOrDefault(u => u.ArticleId == id);

            _db.Article.Remove(article ?? throw new InvalidOperationException());
            return await _db.SaveChangesAsync();
        }

        public async Task UpdateArticle(UpdateArticleModel updateModel)
        {
            var article = _db.Article.SingleOrDefault(a => a.ArticleId == updateModel.ArticleId);

            if(article == null)
                throw new ArgumentNullException();

            article.Title = updateModel.Title;
            article.Content = updateModel.Content;
            article.ArticleCategoryId = updateModel.ArticleCategoryId;

            await _db.SaveChangesAsync();
        }

        public async Task CreateArticle(CreateArticleModel model)
        {
            var entity = _mapper.Map<ArticleEntity>(model);

            entity.DatePublished = DateTime.Now;

            _db.Article.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task CreateComment(CreateCommentModel model)
        {
            var entity = _mapper.Map<CommentEntity>(model);

            entity.DatePublished = DateTime.Now;

            _db.Comment.Add(entity);
            await _db.SaveChangesAsync();
        }

        public List<CommentEntity> GetAllComments()
        {
            var comments = _db.Comment.OrderByDescending(x => x.DatePublished).ToList();

            return comments;
        }
    }
}
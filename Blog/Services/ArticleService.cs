using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entities;
using Blog.Infrastructure.Database;
using Blog.Models.Article;
using Blog.Models.Article.Comment;

namespace Blog.Services
{
    public interface IArticleService
    {
        List<ArticleEntity> GetAllArticles();
        List<ArticleEntity> GetAllArticlesForCategory(ArticleCategory category);
        Task CreateArticle(CreateArticleModel model);
        ArticleEntity GetArticleById(int id);
        List<CommentEntity> GetCommentsForArticleId(int articleId);
        string GetArticleTitleById(int id);
        Task<int> DeleteArticle(int id);
        Task CreateComment(CreateCommentModel createModel);
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

        public ArticleEntity GetArticleById(int id)
        {
            return _db.Article.SingleOrDefault(a => a.ArticleId == id);
        }

        public List<CommentEntity> GetCommentsForArticleId(int articleId)
        {
            return _db.Comment.Where(c => c.ArticleId == articleId).ToList();
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

        public async Task CreateArticle(CreateArticleModel model)
        {
            var entity = _mapper.Map<ArticleEntity>(model);

            entity.DatePublished = DateTime.Now;

            _db.Article.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task CreateComment(CreateCommentModel createModel)
        {
            var entity = _mapper.Map<CommentEntity>(createModel);

            entity.DatePublished = DateTime.Now;

            _db.Comment.Add(entity);
            await _db.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entities;
using Blog.Helpers;
using Blog.Models.Article;

namespace Blog.Services.Article
{
    public interface IArticleService
    {
        List<ArticleEntity> GetAllArticles();
        List<ArticleEntity> GetAllArticlesForCategory(ArticleCategory category);
        Task CreateArticle(CreateArticleModel model);
        string GetArticleTitleById(int id);
        Task<int> DeleteArticle(int id);
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

        public async Task CreateArticle(CreateArticleModel model)
        {
            var entity = _mapper.Map<ArticleEntity>(model);

            entity.DatePublished = DateTime.Now;

            _db.Article.Add(entity);
            await _db.SaveChangesAsync();
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
    }
}
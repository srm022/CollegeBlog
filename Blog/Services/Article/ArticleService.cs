using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entities;
using Blog.Helpers;
using Blog.Models;

namespace Blog.Services.Article
{
    public interface IArticleService
    {
        List<ArticleEntity> GetAllArticles();
        Task CreateArticle(CreateArticleModel model);
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

        public async Task CreateArticle(CreateArticleModel model)
        {
            var entity = _mapper.Map<ArticleEntity>(model);

            AttachDateAndAuthor(entity);

            _db.Article.Add(entity);
            await _db.SaveChangesAsync();
        }

        private void AttachDateAndAuthor(ArticleEntity entity)
        {
            entity.DatePublished = DateTime.Now;
        }
    }
}
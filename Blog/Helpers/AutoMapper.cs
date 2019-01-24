using AutoMapper;
using Blog.Entities;
using Blog.Models;
using Blog.Models.Article;

namespace Blog.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DisplayArticleModel, ArticleEntity>();
            CreateMap<ArticleEntity, DisplayArticleModel>();

            CreateMap<CreateArticleModel, ArticleEntity>();
            CreateMap<ArticleEntity, CreateArticleModel>();
        }
    }
}
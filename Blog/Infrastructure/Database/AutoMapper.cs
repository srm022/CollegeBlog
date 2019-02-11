using AutoMapper;
using Blog.Entities;
using Blog.Models.Article.Comment;
using Blog.Models.PageContent.Article;

namespace Blog.Infrastructure.Database
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DisplayArticleModel, ArticleEntity>();
            CreateMap<ArticleEntity, DisplayArticleModel>();

            CreateMap<CreateArticleModel, ArticleEntity>();
            CreateMap<ArticleEntity, CreateArticleModel>();

            CreateMap<UpdateArticleModel, ArticleEntity>();
            CreateMap<ArticleEntity, UpdateArticleModel>();

            CreateMap<CreateCommentModel, CommentEntity>();
            CreateMap<CommentEntity, CreateCommentModel>();
        }
    }
}
using AutoMapper;
using Blog.Entities;
using Blog.Models.Article;
using Blog.Models.Article.Comment;

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

            CreateMap<CreateCommentModel, CommentEntity>();
            CreateMap<CommentEntity, CreateCommentModel>();
        }
    }
}
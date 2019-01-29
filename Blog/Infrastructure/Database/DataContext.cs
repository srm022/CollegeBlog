using Blog.Entities;
using Blog.Models.Article.Comment;
using Blog.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ArticleEntity> Article { get; set; }
        public DbSet<CommentEntity> Comment { get; set; }
    }
}
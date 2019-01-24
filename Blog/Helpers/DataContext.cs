using Blog.Entities;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<Role> Roles { get; set; }
 
        public DbSet<UserEntity> Users { get; set; }
 
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
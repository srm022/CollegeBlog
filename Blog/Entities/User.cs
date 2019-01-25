using System.ComponentModel.DataAnnotations;

namespace Blog.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PasswordHash { get; set; }
    }
}
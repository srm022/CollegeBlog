using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(RegisterModel))]
        public int UserId { get; set; }

        [Required, ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
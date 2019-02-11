using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.User
{
    public class RegisterModel
    {
        [Key, Required]
        public int Id { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [MaxLength(32)]
        public string DisplayName { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [NotMapped]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match."), Required]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
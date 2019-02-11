using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Contact
{
    public class ContactFormModel
    {
        [MaxLength(32)]
        public string SenderName { get; set; }

        [MaxLength(64)]
        public string Subject { get; set; }

        [EmailAddress, Required]
        public string SenderMail { get; set; }

        public DateTime TimeStamp { get; set; }

        [Required, MinLength(10)]
        public string Content { get; set; }
    }
}
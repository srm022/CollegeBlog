using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models.Article;
using Blog.Models.Contact;
using Blog.Models.PageContent.Article;
using Blog.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IContactService _contactService;

        [BindProperty]
        public ContactFormModel Model { get; set; }

        public ContactModel(IContactService contactService)
        {
            _contactService = contactService;
            Model = new ContactFormModel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                AttachTimestamp();
                _contactService.SendMail(Model);
            }

            RedirectToPage("/Index");
        }

        private void AttachTimestamp()
        {
            Model.TimeStamp = DateTime.Now;
        }
    }
}
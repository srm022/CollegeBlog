using System;
using System.Threading.Tasks;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public string Name { get; set; }

        public void OnGet()
        {
            Name = _userService.GetUsernameById(Id);
        }
        
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _userService.DeleteUser(Id);

            return RedirectToPage("/Admin/Users/Index");
        }
    }
}
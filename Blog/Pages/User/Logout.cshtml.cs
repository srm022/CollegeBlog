using System.Threading.Tasks;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.User
{
    public class LogoutModel : PageModel
    {
        private readonly IUserService _service;

        public LogoutModel(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet()
        {
            await _service.Logout();
            return RedirectToPage("/Index");
        }
    }
}
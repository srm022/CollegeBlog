using System.Threading.Tasks;
using Blog.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _service;

        public RegisterModel(IUserService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.RegisterModel Model { get; set; }
        
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _service.Register(Model);
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
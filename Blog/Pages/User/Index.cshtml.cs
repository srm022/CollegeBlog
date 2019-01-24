using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Services.User;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _service;

        public LoginModel(IUserService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.LoginModel Model { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await AuthenticateUser();
                if (result.Succeeded)
                {
                    Console.WriteLine("Login succeeded.");
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }

        private async Task<SignInResult> AuthenticateUser()
        {
            return await _service.Authenticate(Model.Email, Model.Password);
        }
    }
}

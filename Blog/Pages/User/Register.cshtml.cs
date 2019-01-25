using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Blog.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Blog.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;

        public RegisterModel(IUserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            ViewData["ReCaptchaKey"] = _configuration.GetSection("GoogleReCaptcha:key").Value;
            return Page();
        }

        [BindProperty]
        public Models.RegisterModel Model { get; set; }
        
        public async Task<IActionResult> OnPost()
        {
            //ViewData["ReCaptchaKey"] = _configuration.GetSection("GoogleReCaptcha:key").Value;

            if (ModelState.IsValid)
            {
                //if (!ReCaptchaPassed(
                //    Request.Form["g-recaptcha-response"],
                //    _configuration.GetSection("GoogleReCaptcha:secret").Value))
                //{
                //    ModelState.AddModelError(string.Empty, "Captcha failed.");
                //    return Page();
                //}

                await _service.Register(Model);
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
                return false;

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class TestController : Controller
    {
        public IActionResult NewView()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
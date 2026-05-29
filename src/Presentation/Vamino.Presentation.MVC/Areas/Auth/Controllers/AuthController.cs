using Microsoft.AspNetCore.Mvc;

namespace Vamino.Presentation.MVC.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Route("Auth")]
    public class AuthController : Controller
    {
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}

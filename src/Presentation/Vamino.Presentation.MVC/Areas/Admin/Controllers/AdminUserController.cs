using Microsoft.AspNetCore.Mvc;

namespace Vamino.Presentation.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

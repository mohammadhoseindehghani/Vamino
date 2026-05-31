using Microsoft.AspNetCore.Mvc;

namespace Vamino.Presentation.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}

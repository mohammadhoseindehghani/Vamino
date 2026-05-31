using Microsoft.AspNetCore.Mvc;

namespace Vamino.Presentation.MVC.Areas.Admin.Controllers
{
    public class AdminGuarantorsController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

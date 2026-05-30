using Microsoft.AspNetCore.Mvc;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class GuarantorRequestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
